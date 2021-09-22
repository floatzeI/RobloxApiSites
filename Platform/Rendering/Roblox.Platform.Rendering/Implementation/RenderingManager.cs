using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Roblox.Avatar.Client.Models;
using Roblox.Files.Client;
using Roblox.Rendering.Client;
using Roblox.Rendering.Client.Models;
using Roblox.Thumbnails.Client;
using Roblox.Thumbnails.Client.Models;

namespace Roblox.Platform.Rendering
{
    public class RenderingManager : IRenderingManager
    {
        private IFilesV1Client filesClient { get; set; }
        private IThumbnailsV1Client thumbnailsClient { get; set; }
        private IRenderingClient renderingClient { get; set; }

        public RenderingManager(IFilesV1Client filesV1Client, IThumbnailsV1Client thumbnailsV1Client,
            IRenderingClient renderingClientParam)
        {
            filesClient = filesV1Client;
            thumbnailsClient = thumbnailsV1Client;
            renderingClient = renderingClientParam;
        }
        
        private Stream ConvertBase64ToImage(string base64String)
        {
            return new MemoryStream(Convert.FromBase64String(base64String));
        }

        private string CreateMd5FromAvatar(AvatarEntry entry, string identifier)
        {
            // If you make any changes the output of this function, you MUST increment the version
            var str = $"V1 {identifier} {string.Join(",",entry.assetIds)}{entry.type}";
            if (entry.colors != null)
            {
                var c = entry.colors;
                str += $"colors = {c.headColorId}{c.torsoColorId}{c.leftArmColorId}{c.rightArmColorId}{c.leftLegColorId}{c.rightLegColorId}";
            }

            if (entry.scales != null)
            {
                var s = entry.scales;
                str += $"scales = {s.depth}{s.head}{s.height}{s.proportion}{s.width}{s.bodyType}";
            }

            var byteArray = Encoding.UTF8.GetBytes(str);
            var hashBytes = MD5.Create().ComputeHash(byteArray);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        public async Task DeleteAvatarThumbnails(long userId)
        {
            await thumbnailsClient.DeleteThumbnail(userId, ThumbnailType.AvatarHeadshot);
            await thumbnailsClient.DeleteThumbnail(userId, ThumbnailType.AvatarThumbnail);
        }
        
        public async Task RenderAvatarThumbnail(long userId, int resolution, AvatarEntry avatar, bool force = false)
        {
            // temporary until resolutions are added to RenderingClient
            if (resolution != 420) throw new ArgumentException("Unsupported resolution");
            
            var imageId = CreateMd5FromAvatar(avatar, "Thumbnail");

            if (!force)
            {
                // check if it already exists
                try
                {
                    var exists = await thumbnailsClient.GetThumbnailByHash(imageId, resolution, resolution);
                    // Add the existing thumb and return
                    await thumbnailsClient.InsertThumbnail(new(imageId, userId, exists.fileId, ThumbnailType.AvatarThumbnail, resolution, resolution));
                    return;

                }
                catch (ThumbnailNotFoundException)
                {
                    // Ignore
                }
            }

            var result = await renderingClient.RenderAvatarThumbnail(new RenderAvatarRequest()
            {
                assets = avatar.assetIds.Select(c => new RenderAssetEntry()
                {
                    id = c,
                }),
                playerAvatarType = avatar.type.ToString(),
                bodyColors = new()
                {
                    headColorId = avatar.colors.headColorId,
                    torsoColorId = avatar.colors.torsoColorId,
                    leftArmColorId = avatar.colors.leftArmColorId,
                    rightArmColorId = avatar.colors.rightArmColorId,
                    leftLegColorId = avatar.colors.leftLegColorId,
                    rightLegColorId = avatar.colors.rightLegColorId,
                },
                userId = userId,
            });
            var imageStream = ConvertBase64ToImage(result.fileBase64);
            // upload the image
            var fileId = await filesClient.UploadFile("image/png", imageStream);
            await thumbnailsClient.InsertThumbnail(new(imageId, userId, fileId, ThumbnailType.AvatarThumbnail, resolution, resolution));
        }
        
        public async Task RenderAvatarHeadshot(long userId, int resolution, AvatarEntry avatar, bool force = false)
        {
            // temporary until resolutions are added to RenderingClient
            if (resolution != 420) throw new ArgumentException("Unsupported resolution");
            
            var imageId = CreateMd5FromAvatar(avatar, "Headshot");

            if (!force)
            {
                // check if it already exists
                try
                {
                    var exists = await thumbnailsClient.GetThumbnailByHash(imageId, resolution, resolution);
                    // Add the existing thumb and return
                    await thumbnailsClient.InsertThumbnail(new(imageId, userId, exists.fileId, ThumbnailType.AvatarThumbnail, resolution, resolution));
                    return;

                }
                catch (ThumbnailNotFoundException)
                {
                    // Ignore
                }
            }

            var result = await renderingClient.RenderAvatarHeadshot(new RenderAvatarRequest()
            {
                assets = avatar.assetIds.Select(c => new RenderAssetEntry()
                {
                    id = c,
                }),
                playerAvatarType = avatar.type.ToString(),
                bodyColors = new()
                {
                    headColorId = avatar.colors.headColorId,
                    torsoColorId = avatar.colors.torsoColorId,
                    leftArmColorId = avatar.colors.leftArmColorId,
                    rightArmColorId = avatar.colors.rightArmColorId,
                    leftLegColorId = avatar.colors.leftLegColorId,
                    rightLegColorId = avatar.colors.rightLegColorId,
                },
                userId = userId,
            });
            var imageStream = ConvertBase64ToImage(result.fileBase64);
            // upload the image
            var fileId = await filesClient.UploadFile("image/png", imageStream);
            await thumbnailsClient.InsertThumbnail(new(imageId, userId, fileId, ThumbnailType.AvatarThumbnail, resolution, resolution));
        }
    }
}