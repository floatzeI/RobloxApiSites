using System;
using System.Threading.Tasks;
using Roblox.Services.Controllers;
using Xunit;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;
using Roblox.Services.Services;
using Range = Moq.Range;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestUsersService
    {
        [Fact]
        public async Task Get_Description_For_User()
        {
            var userId = 1;
            var description = "Service unit test description entry";
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.GetAccountInformationEntry(userId)).ReturnsAsync(new AccountInformationEntry()
            {
                userId = userId,
                description = description,
            });
            var service = new UsersService(mock.Object);
            var result = await service.GetDescription(userId);
            Assert.Equal(description, result.description);
            Assert.Equal(userId, result.userId);
            
            mock.VerifyAll();
        }
        
        [Fact]
        public async Task Get_Description_For_User_Without_Record()
        {
            var userId = 1;
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.GetAccountInformationEntry(userId)).ReturnsAsync((AccountInformationEntry)null);
            var service = new UsersService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetDescription(userId);
            });
            
            mock.VerifyAll();
        }
        
        [Fact]
        public async Task Set_Description_For_User_With_Record()
        {
            var userId = 1;
            var newDescription = "This is my new description for unit test";
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.DoesHaveAccountInformationEntry(userId)).ReturnsAsync(true);
            mock.Setup(c => c.UpdateUserDescription(userId, newDescription));
            
            var service = new UsersService(mock.Object);
            await service.SetUserDescription(userId, newDescription);
            
            mock.VerifyAll();
        }
        
                
        [Fact]
        public async Task Set_Description_For_User_Without_Record()
        {
            var userId = 1;
            var newDescription = "This is my new description for unit test";
            var expectedInsertRecord = new AccountInformationEntry()
            {
                userId = userId,
                description = newDescription,
            };
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.DoesHaveAccountInformationEntry(userId)).ReturnsAsync(false);
            mock.Setup(c => c.InsertAccountInformationEntry(It.IsAny<AccountInformationEntry>()));

            var service = new UsersService(mock.Object);
            await service.SetUserDescription(userId, newDescription);
            
            mock.VerifyAll();
        }
    }
}