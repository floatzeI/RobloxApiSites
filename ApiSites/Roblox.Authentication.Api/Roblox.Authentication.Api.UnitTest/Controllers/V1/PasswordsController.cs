using System.Threading.Tasks;
using Moq;
using Roblox.Authentication.Api.Controllers;
using Roblox.Authentication.Api.Models;
using Roblox.Passwords.Client;
using Roblox.Web.Authentication.Passwords;
using Roblox.Web.WebAPI.Exceptions;
using Xunit;

namespace Roblox.Authentication.Api.UnitTest.Controllers.V1
{
    public class UnitTestPasswordsController
    {
        [Fact]
        public void Return_True_For_Good_Password()
        {
            var controller = new PasswordsController(null);
            var result = controller.ValidateFromUri(new()
            {
                password = "$ecureP@$$w0rd1234",
                username = "Roblox",
            });
            Assert.Equal(PasswordValidationStatus.ValidPassword, result.code);
        }
        
        [Fact]
        public void Return_True_For_Good_Password_FromBody()
        {
            var controller = new PasswordsController(null);
            var result = controller.ValidateFromBody(new()
            {
                password = "$ecureP@$$w0rd1234",
                username = "Roblox",
            });
            Assert.Equal(PasswordValidationStatus.ValidPassword, result.code);
        }
        
        [Fact]
        public void Return_False_For_Bad_Passwords()
        {
            var controller = new PasswordsController(null);
            Assert.Equal(PasswordValidationStatus.DumbStringsError, controller.ValidateFromUri(new()
            {
                password = "roblox123",
                username = "Roblox",
            }).code);
            Assert.Equal(PasswordValidationStatus.ShortPasswordError, controller.ValidateFromUri(new()
            {
                password = "road",
                username = "Roblox",
            }).code);
            Assert.Equal(PasswordValidationStatus.PasswordSameAsUsernameError, controller.ValidateFromUri(new()
            {
                password = "RobloxRoblox",
                username = "RobloxRoblox",
            }).code);
            Assert.Equal(PasswordValidationStatus.WeakPasswordError, controller.ValidateFromUri(new()
            {
                password = "RobloxRoblox",
                username = "Roblox",
            }).code);
        }

        [Fact]
        public async Task Change_Password_For_Authenticated_User()
        {
            var oldPass = "OldPassword123";
            var newPass = "NewPa$$w0rd1234567";
            var userId = 1;
            var passwordsClient = new Mock<IPasswordsV1Client>();
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, oldPass)).ReturnsAsync(true);
            
            var controller = new PasswordsController(passwordsClient.Object)
            {
                _userOverrideForTests = new()
                {
                    id = userId,
                }
            };
            
            var result = await controller.ChangePassword(new PasswordChangeModel()
            {
                currentPassword = oldPass,
                newPassword = newPass,
            });
            
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, oldPass), Times.Once);
            passwordsClient.Verify(c => c.SetPassword(userId, newPass), Times.Once);
        }
        
        [Fact]
        public async Task Change_Password_Fail_Due_To_Incorrect_Current_Password()
        {
            var oldPass = "OldPassword123";
            var newPass = "NewPa$$w0rd1234567";
            var userId = 1;
            var passwordsClient = new Mock<IPasswordsV1Client>();
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, oldPass)).ReturnsAsync(false);
            
            var controller = new PasswordsController(passwordsClient.Object)
            {
                _userOverrideForTests = new()
                {
                    id = userId,
                }
            };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await controller.ChangePassword(new PasswordChangeModel()
                {
                    currentPassword = oldPass,
                    newPassword = newPass,
                });
            });
            
            Assert.Equal((int)PasswordResponseCodes.InvalidCurrentPassword, exception.code);
            
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, oldPass), Times.Once);
            passwordsClient.Verify(c => c.SetPassword(userId, newPass), Times.Never);
        }
        
        [Fact]
        public async Task Change_Password_Fail_Due_To_New_Pass_Too_Weak()
        {
            var oldPass = "OldPassword123";
            var newPass = "weakpassword";
            var userId = 1;
            var passwordsClient = new Mock<IPasswordsV1Client>();
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, oldPass)).ReturnsAsync(true);
            
            var controller = new PasswordsController(passwordsClient.Object)
            {
                _userOverrideForTests = new()
                {
                    id = userId,
                }
            };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await controller.ChangePassword(new PasswordChangeModel()
                {
                    currentPassword = oldPass,
                    newPassword = newPass,
                });
            });
            Assert.Equal((int)PasswordResponseCodes.InvalidPassword, exception.code);
            
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, oldPass), Times.Once);
            passwordsClient.Verify(c => c.SetPassword(userId, newPass), Times.Never);
        }
    }
}