using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Roblox.Services.Controllers.V1;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestFilesController
    {
        [Fact]
        public async Task Upload_One_File()
        {
            var file = new Mock<IFormFile>();
            file.Setup(c => c.OpenReadStream()).Returns((Stream)null);
            var mime = "image/png";

            var hash = "fakehash123";
            var mock = new Mock<IFilesService>();
            mock.Setup(c => c.CreateFileHash(null)).ReturnsAsync(hash);
            mock.Setup(c => c.UploadFile(It.IsAny<Stream>(), hash, mime));
            
            var controller = new FilesController(mock.Object);
            var result = await controller.UploadFile(new ()
            {
                file = file.Object,
                mime = mime,
            });
            Assert.Equal(hash, result.id);
            mock.Verify(c => c.CreateFileHash(null), Times.Once);
            mock.Verify(c => c.UploadFile(It.IsAny<Stream>(), hash, mime), Times.Once);
        }
        [Fact]
        public async Task Upload_One_File_Use_Stream_Mime()
        {
            var file = new Mock<IFormFile>();
            file.Setup(c => c.OpenReadStream()).Returns((Stream)null);
            var mime = "image/png";
            file.Setup(c => c.ContentType).Returns(mime);

            var hash = "fakehash123";
            var mock = new Mock<IFilesService>();
            mock.Setup(c => c.CreateFileHash(null)).ReturnsAsync(hash);
            mock.Setup(c => c.UploadFile(It.IsAny<Stream>(), hash, mime));
            
            var controller = new FilesController(mock.Object);
            var result = await controller.UploadFile(new ()
            {
                file = file.Object,
            });
            Assert.Equal(hash, result.id);
            mock.Verify(c => c.CreateFileHash(null), Times.Once);
            mock.Verify(c => c.UploadFile(It.IsAny<Stream>(), hash, mime), Times.Once);
        }
    }
}