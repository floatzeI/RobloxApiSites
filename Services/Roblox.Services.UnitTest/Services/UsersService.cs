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

        [Fact]
        public async Task Get_User_By_Id()
        {
            var userId = 1;
            var name = "UnitTest1234";
            var desc = "Unit test description";
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.GetUserAccountById(userId)).ReturnsAsync(new UserAccountEntry()
            {
                username = name,
                userId = 1,
            });
            mock.Setup(c => c.GetAccountInformationEntry(userId)).ReturnsAsync(new AccountInformationEntry()
            {
                description = desc
            });

            var service = new UsersService(mock.Object);
            var result = await service.GetUserById(userId);
            Assert.Equal(userId, result.userId);
            Assert.Equal(name, result.username);
            Assert.Equal(desc, result.description);
        }
        
        [Fact]
        public async Task Get_User_By_Id_With_Null_Account_Information()
        {
            var userId = 1;
            var name = "UnitTest1234";
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.GetUserAccountById(userId)).ReturnsAsync(new UserAccountEntry()
            {
                username = name,
                userId = 1,
            });
            mock.Setup(c => c.GetAccountInformationEntry(userId)).ReturnsAsync((AccountInformationEntry)null);

            var service = new UsersService(mock.Object);
            var result = await service.GetUserById(userId);
            Assert.Equal(userId, result.userId);
            Assert.Equal(name, result.username);
            Assert.Null(result.description);
        }
                
        [Fact]
        public async Task Get_User_By_Id_Non_Existent()
        {
            var userId = 1;
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.GetUserAccountById(userId)).ReturnsAsync((UserAccountEntry)null);
            var service = new UsersService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetUserById(userId);
            });
        }

        [Fact]
        public void Get_Date_Time_From_Valid_Date()
        {
            var service = new UsersService(null);
            var result = service.GetDateTimeFromBirthDate(2000, 1, 2);
            Assert.Equal(2000, result.Year);
            Assert.Equal(1, result.Month);
            Assert.Equal(2, result.Day);
            Assert.Equal(0, result.Hour);
            Assert.Equal(0, result.Minute);
            Assert.Equal(0, result.Second);
            Assert.Equal(0, result.Millisecond);
        }
        
        [Fact]
        public void Get_Date_Time_From_Bad_Date_And_Throw()
        {
            var service = new UsersService(null);
            Assert.Throws<ParameterException>(() =>
            {
                service.GetDateTimeFromBirthDate(2021, 2, 31);
            });
        }
    }
}