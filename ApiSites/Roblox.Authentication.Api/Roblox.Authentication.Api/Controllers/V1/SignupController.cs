using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Assets.Client;
using Roblox.Authentication.Api.Models;
using Roblox.Authentication.Api.Validators;
using Roblox.Avatar.Client;
using Roblox.Avatar.Client.Models;
using Roblox.Marketplace.Client;
using Roblox.Ownership.Client;
using Roblox.Passwords.Client;
using Roblox.Platform.Avatar;
using Roblox.Platform.Rendering;
using Roblox.Rendering.Client;
using Roblox.Rendering.Client.Models;
using Roblox.Sessions.Client;
using Roblox.Users.Client;
using Roblox.Users.Client.Exceptions;
using Roblox.Web.Authentication.Signup;
using Roblox.Web.WebAPI;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/signup")]
    public class SignupController
    {
        private IUsersV1Client usersClient { get; }
        private IPasswordsV1Client passwordsClient { get; }
        private ISessionsV1Client sessionsClient { get; }
        private IAvatarV1Client avatarClient { get; }
        private IAssetsV1Client assetsClient { get; set; }
        private IMarketplaceV1Client marketplaceClient { get; set; }
        private IOwnershipClient ownershipClient { get; set; }
        private IRenderingClient renderingClient { get; set; }
        private IRenderingManager renderingManager { get; set; }

        public SignupController(IUsersV1Client users, IPasswordsV1Client passwords, ISessionsV1Client sessions, IAvatarV1Client avatar, IAssetsV1Client assets, IMarketplaceV1Client marketplace, IOwnershipClient ownershipClientParam, IRenderingClient renderingClientParam, IRenderingManager renderingManager)
        {
            usersClient = users;
            passwordsClient = passwords;
            sessionsClient = sessions;
            avatarClient = avatar;
            assetsClient = assets;
            marketplaceClient = marketplace;
            ownershipClient = ownershipClientParam;
            renderingClient = renderingClientParam;
            this.renderingManager = renderingManager;
        }

        private SignupRequest ValidateScales(Models.SignupRequest request)
        {
            if (!AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.head, request.headScale))
            {
                request.headScale = 1;
            }

            if (!AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.height, request.heightScale))
            {
                request.heightScale = 1;
            }
            if (!AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.width, request.widthScale))
            {
                request.widthScale = 1;
            }

            if (!AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.proportion, request.proportionScale))
            {
                request.proportionScale = 0;
            }
            if (!AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.bodyType, request.bodyTypeScale))
            {
                request.bodyTypeScale = 0;
            }

            return request;
        }
        
        /// <summary>
        /// Endpoint for signing up a new user
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">
        /// Successful signup
        /// </response>
        /// <response code="400">
        /// Bad request
        /// 16: User agreement ids are null.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// 2: Captcha Failed.
        /// 4: Invalid Birthday.
        /// 5: Invalid Username.
        /// 6: Username already taken.
        /// 7: Invalid Password.
        /// 8: Password and Username are same.
        /// 9: Password is too simple.
        /// 10: Email is invalid.
        /// 11: Asset is invalid.
        /// 12: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="429">
        /// 3: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="500">
        /// Internal server error
        /// 15: Insert acceptances failed.
        /// </response>
        /// <response code="503">
        /// Service unavailable
        /// </response>
        [HttpPost]
        public async Task<Models.SignupResponse> Signup(Models.SignupRequest request)
        {
            // todo: what has to be called:
            // Done: 1. Create user with UsersService
            // Done: 2. insert gender
            // 3. Insert locale info for that user
            // Done: 4. Insert password for that user
            // 5. If email specified, insert email record
            // Done: 6. Create avatar (go off params if specified, otherwise use default)
            // Done: 7. Add default avatar items to inventory, plus items specified in request.assetIds (as long as they're free items)
            // Done: 8. add thumbnail and headshot
            // 9. Create default place and universe for the user
            // Done: 10. Finally, Create session and set cookie
            
            // In the future, we may also want to look into captcha verification (mostly for realism)
            
            // implementation start
            try
            {
                await usersClient.GetUserByUsername(request.username);
                throw new ForbiddenException(SignupResponseCodes.UsernameTaken, "Username already taken.");
            }
            catch (UserNotFoundException)
            {
                // Good
            }

            DateTime birthDate;
            try
            {
                birthDate = DateTime.Parse(request.birthday);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ForbiddenException(SignupResponseCodes.InvalidBirthDay, "Invalid Birthday.");
            }

            var birthdayErrors = BirthdayValidator.ValidateBirthday(birthDate);
            if (birthdayErrors != null)
            {
                throw new ForbiddenException(SignupResponseCodes.InvalidBirthDay, "Invalid Birthday.");
            }

            var passwordErrors = PasswordValidator.GetPasswordStatus(request.username, request.password);
            if (passwordErrors.code != PasswordValidationStatus.ValidPassword)
            {
                throw new ForbiddenException(SignupResponseCodes.PasswordTooSimple, "Password is too simple.");
            }
            
            // validate scales
            request = ValidateScales(request);

            var user = await usersClient.InsertUser(request.username, birthDate.Year, birthDate.Month, birthDate.Day);
            try
            {
                // set pass
                await passwordsClient.SetPassword(user.userId, request.password);
                // set account gender
                await usersClient.SetGender(user.userId, request.gender);
                // set avatar
                // first, filter out the assets request to only include items that are for sale, exist, are free, and are wearable
                var assetDetails = await assetsClient.MultiGetAssetById(request.assetIds);
                assetDetails = assetDetails.Where(c => AvatarValidator.IsWearable(c.assetTypeId));
                // Remove items that are not free and not for sale
                var productDetails = await marketplaceClient.GetProductsByAssetId(assetDetails.Select(c => c.assetId));
                productDetails = productDetails.Where(c => c.isFree && c.isForSale);
                // Create a new array of valid items the user can have
                var acceptedAssetIds = productDetails.Select(c => c.assetId).ToArray();
                var insertOwnershipTasks = new List<Task>();
                foreach (var item in acceptedAssetIds)
                {
                    insertOwnershipTasks.Add(ownershipClient.CreateEntry(new()
                    {
                        userId = user.userId,
                        assetId = item,
                        serialNumber = null,
                        expires = null,
                    }));
                }

                await Task.WhenAll(insertOwnershipTasks);

                var colorId = request.bodyColorId;
                var colorOk = AvatarValidator.IsBrickColorValid(colorId);
                var av = new SetAvatarRequest()
                {
                    userId = user.userId,
                    scales = new()
                    {
                        proportion = request.proportionScale,
                        bodyType = request.bodyTypeScale,
                        height = request.heightScale,
                        width = request.widthScale,
                        head = request.headScale,
                        depth = 1,
                    },
                    type = AvatarType.R15,
                    colors = new()
                    {
                        headColorId = colorOk ? colorId : 208,
                        torsoColorId = colorOk ? colorId : 21,
                        rightArmColorId = colorOk ? colorId : 208,
                        leftArmColorId = colorOk ? colorId : 208,
                        rightLegColorId = colorOk ? colorId : 102,
                        leftLegColorId = colorOk ? colorId : 102,
                    },
                    assetIds = acceptedAssetIds,
                    /*
                    assetIds = new long[]
                    {
                        // 63690008, // Pal hair
                        // 86498048, // man head
                        // 86500008, // man torso
                        // 86500036, // man right arm
                        // 86500054, // man left arm
                        // 86500064, // man left leg
                        // 86500078, // man right leg
                        // 144075659, // smile
                        // 144076358, // blue and black motorcycle shirt
                        // 144076760, // dark green jeans
                    },
                    */
                };
                await avatarClient.SetUserAvatar(av);
                var defaultResolution = 420;
                JobQueue.Schedule("RenderAvatarThumbnail,Headshot",async () =>
                {
                    await Task.WhenAll(new List<Task>()
                    {
                        renderingManager.RenderAvatarHeadshot(user.userId, defaultResolution, av),
                        renderingManager.RenderAvatarThumbnail(user.userId, defaultResolution, av)
                    });
                });
            }
            catch (Exception e)
            {
                // capture, then rollback
                await usersClient.DeleteUser(user.userId);

                throw new Exception("Signup Failed", e);
            }

            return new()
            {
                userId = user.userId,
            };
        }
    }
}