using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestFilesService
    {
        [Fact]
        public async Task Upload_One_File()
        {
            var hash = "fakehash123";
            var mime = "Image/PNG";
            var byteLength = 123;
            
            var stream = new Mock<Stream>();
            stream.Setup(c => c.Length).Returns(byteLength);
            
            var db = new Mock<IFilesDatabase>();
            db.Setup(c => c.DoesFileExist(hash)).ReturnsAsync(false);
            var storage = new Mock<IFilesStorageDatabase>();

            var service = new FilesService(db.Object, storage.Object);
            await service.UploadFile(stream.Object, hash, mime);
            db.Verify(c => c.InsertFile(hash, mime, byteLength), Times.Once);
            db.Verify(c => c.DoesFileExist(hash), Times.Once);
            storage.Verify(c => c.UploadFile(hash, stream.Object, mime), Times.Once);
        }
        
        [Fact]
        public async Task Upload_One_File_That_Already_Exists()
        {
            var hash = "fakehash123";
            var mime = "Image/PNG";
            var byteLength = 123;
            
            var stream = new Mock<Stream>();
            stream.Setup(c => c.Length).Returns(byteLength);
            
            var db = new Mock<IFilesDatabase>();
            db.Setup(c => c.DoesFileExist(hash)).ReturnsAsync(true);
            var storage = new Mock<IFilesStorageDatabase>();

            var service = new FilesService(db.Object, storage.Object);
            await service.UploadFile(stream.Object, hash, mime);
            db.Verify(c => c.InsertFile(hash, mime, byteLength), Times.Never);
            db.Verify(c => c.DoesFileExist(hash), Times.Once);
            storage.Verify(c => c.UploadFile(hash, stream.Object, mime), Times.Never);
        }
        
        [Fact]
        public async Task Upload_Fail_And_Rollback()
        {
            var hash = "fakehash123";
            var mime = "Image/PNG";
            var byteLength = 123;
            
            var stream = new Mock<Stream>();
            stream.Setup(c => c.Length).Returns(byteLength);
            
            var db = new Mock<IFilesDatabase>();
            db.Setup(c => c.DoesFileExist(hash)).ReturnsAsync(false);
            db.Setup(c => c.InsertFile(hash, mime, byteLength))
                .ThrowsAsync(new Exception("Example exception here (e.g. duplicate key or db unavailable or something)"));
            var storage = new Mock<IFilesStorageDatabase>();

            var service = new FilesService(db.Object, storage.Object);
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await service.UploadFile(stream.Object, hash, mime);
            });
            db.Verify(c => c.InsertFile(hash, mime, byteLength), Times.Once);
            db.Verify(c => c.DoesFileExist(hash), Times.Once);
            storage.Verify(c => c.UploadFile(hash, stream.Object, mime), Times.Once);
            storage.Verify(c => c.DeleteFile(hash), Times.Once);
        }
        
        [Fact]
        public async Task Create_File_Hash()
        {
            var expectedHash = "9e6865a0697646f7d2734846e229ec0a";
            var stream = new MemoryStream(new UTF8Encoding().GetBytes("Example string for a file here"));

            var service = new FilesService(null, null);
            var result = await service.CreateFileHash(stream);
            Assert.Equal(expectedHash, result);
        }
    }
}