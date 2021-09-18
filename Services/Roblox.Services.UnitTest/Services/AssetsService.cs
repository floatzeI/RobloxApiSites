using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Assets;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestAssetsService
    {
        [Fact]
        public async Task Multi_Get_Assets()
        {
            var mock = new Mock<IAssetsDatabase>();
            var ids = new List<long>()
            {
                1, 2
            };
            mock.Setup(c => c.MultiGetAssetsById(ids)).ReturnsAsync(new List<AssetEntry>()
            {
                new()
                {
                    assetId = 1,
                    name = "Asset One",
                },
                new()
                {
                    assetId = 2,
                    name = "Asset Two"
                },
            });
            var service = new AssetsService(mock.Object);
            var result = await service.MultiGetAssetsById(ids);
            Assert.Equal(2, result.Count());
        }
        
        [Fact]
        public async Task Multi_Get_Assets_With_Absurd_Amount_And_Fail()
        {
            var mock = new Mock<IAssetsDatabase>();
            var ids = new List<long> {};
            for (var i = 0; i < 1000; i++)
            {
                ids.Add(i + 100);
            }

            var service = new AssetsService(mock.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.MultiGetAssetsById(ids);
            });
        }
                
        [Fact]
        public async Task Multi_Get_Assets_With_No_Ids_And_Fail()
        {
            var mock = new Mock<IAssetsDatabase>();
            var ids = new List<long> {};
            
            var service = new AssetsService(mock.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.MultiGetAssetsById(ids);
            });
        }
        
        [Fact]
        public async Task Get_One_Asset_By_Id()
        {
            var mock = new Mock<IAssetsDatabase>();
            var id = 1L;
            mock.Setup(c => c.MultiGetAssetsById(It.IsAny<IEnumerable<long>>())).ReturnsAsync(new List<AssetEntry>()
            {
                new()
                {
                    assetId = 1,
                    name = "Asset One"
                },
            });
            var service = new AssetsService(mock.Object);
            var result = await service.GetAssetById(id);
            Assert.Equal("Asset One", result.name);
            Assert.Equal(1, result.assetId);
        }
        
        [Fact]
        public async Task Get_One_NonExistent_Asset_By_Id_And_Fail()
        {
            var mock = new Mock<IAssetsDatabase>();
            var id = 1L;
            mock.Setup(c => c.MultiGetAssetsById(It.IsAny<IEnumerable<long>>())).ReturnsAsync(new List<AssetEntry>()
            {

            });
            var service = new AssetsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetAssetById(id);
            });
        }
        
        [Fact]
        public async Task Insert_Asset_And_Return_Correct_Data()
        {
            var insertionRequest = new InsertAssetRequest()
            {
                name = "Hello World",
                description = "Big description 1234 big big description description longer than item name unit test1",
                assetType = 6,
                creatorId = 1,
                creatorType = 1,
            };
            
            var expectedCreationDate = DateTime.Now.Subtract(
                TimeSpan.FromSeconds(69)); // random time so we know it isn't being set somewhere
            var expectedAssetId = 123;
            
            var returnResponse = new InsertAssetResponse()
            {
                assetId = expectedAssetId,
                created = expectedCreationDate,
            };

            var mock = new Mock<IAssetsDatabase>();
            mock.Setup(c => c.InsertAsset(insertionRequest)).ReturnsAsync(returnResponse);
            var service = new AssetsService(mock.Object);
            var result = await service.InsertAsset(insertionRequest);
            Assert.Equal(expectedCreationDate, result.created);
            Assert.Equal(expectedAssetId, result.assetId);
        }
        
                
        [Fact]
        public async Task Update_Asset()
        {
            var updateRequest = new UpdateAssetRequest()
            {
                name = "Hello World",
                description = "Big description 1234 big big description description longer than item name unit test1",
                creatorId = 1,
                creatorType = 1,
            };
            
            var mock = new Mock<IAssetsDatabase>();
            mock.Setup(c => c.UpdateAsset(updateRequest));
            var service = new AssetsService(mock.Object);
            await service.UpdateAsset(updateRequest);
            mock.Verify(c => c.UpdateAsset(updateRequest), Times.Once);
        }

        [Fact]
        public async Task Get_Asset_Genres()
        {
            var assetId = 123;
            var mock = new Mock<IAssetsDatabase>();
            mock.Setup(c => c.GetAssetGenres(assetId)).ReturnsAsync(new List<int>() { 1, 2, 3 });
            var service = new AssetsService(mock.Object);
            var result = await service.GetAssetGenres(assetId);
            Assert.Equal(3, result.Count());
            mock.Verify(c => c.GetAssetGenres(assetId), Times.Once);
        }
        
        [Fact]
        public async Task Set_Asset_Genres_No_Previous()
        {
            var assetId = 123;
            var genres = new List<int>()
            {
                1, 2, 3
            };
            var mock = new Mock<IAssetsDatabase>();
            mock.Setup(c => c.GetAssetGenres(assetId)).ReturnsAsync(new int[] { });
            var service = new AssetsService(mock.Object);
            await service.SetAssetGenres(assetId, genres);
            mock.Verify(c => c.GetAssetGenres(assetId), Times.Once);
            mock.Verify(c => c.InsertAssetGenres(assetId, It.IsAny<IEnumerable<int>>()), Times.Once);
            mock.Verify(c => c.DeleteAssetGenres(assetId, It.IsAny<IEnumerable<int>>()), Times.Never);
        }
                
        [Fact]
        public async Task Set_Asset_Genres_With_Previous()
        {
            var assetId = 123;
            var genres = new List<int>()
            {
                1, 3, 4 // adding ID=4, deleting ID=2
            };
            var mock = new Mock<IAssetsDatabase>();
            mock.Setup(c => c.GetAssetGenres(assetId)).ReturnsAsync(new int[]
            {
                1,
                2,
                3,
            });
            mock.Setup(c => c.InsertAssetGenres(assetId, It.IsAny<IEnumerable<int>>())).Callback<long, IEnumerable<int>>((id, genres) =>
            {
                var ids = genres as int[] ?? genres.ToArray();
                Assert.Single(ids);
                Assert.Equal(4, ids[0]);
                Assert.Equal(assetId, id);
            });
            mock.Setup(c => c.DeleteAssetGenres(assetId, It.IsAny<IEnumerable<int>>())).Callback<long, IEnumerable<int>>((id, genres) =>
            {
                var ids = genres as int[] ?? genres.ToArray();
                Assert.Single(ids);
                Assert.Equal(2, ids[0]);
                Assert.Equal(assetId, id);
            });
            var service = new AssetsService(mock.Object);
            await service.SetAssetGenres(assetId, genres);
            mock.Verify(c => c.GetAssetGenres(assetId), Times.Once);
            mock.Verify(c => c.InsertAssetGenres(assetId, It.IsAny<IEnumerable<int>>()), Times.Once);
            mock.Verify(c => c.DeleteAssetGenres(assetId, It.IsAny<IEnumerable<int>>()), Times.Once);
        }
    }
}