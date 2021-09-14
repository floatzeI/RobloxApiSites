using System;
using System.Threading.Tasks;
using Roblox.Services.Controllers.V1;
using Xunit;
using Moq;
using Roblox.Platform.Membership;
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

        [Fact]
        public async Task Create_User_With_Good_Args()
        {
            // arguments
            var username = "GoodName123";
            // expectations
            var expectedUserId = 456;
            // mocks
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.CreateLock(It.IsAny<string>(), It.IsAny<TimeSpan>()));
            mock.Setup(c => c.GetUsersByUsername(username)).ReturnsAsync(new SkinnyUserAccountEntry[] { });
            mock.Setup(c => c.InsertUser(username)).ReturnsAsync(new UserAccountEntry()
            {
                username = username,
                userId = expectedUserId,
                accountStatus = AccountStatus.Ok
            });
            // test
            var service = new UsersService(mock.Object);
            var result = await service.CreateUser(username);
            // assertions
            Assert.Equal(expectedUserId, result.userId);
            Assert.Equal(username, result.username);
            Assert.Equal(AccountStatus.Ok, result.accountStatus);
            // verify
            mock.Verify(c => c.CreateLock(It.IsAny<string>(), It.IsAny<TimeSpan>()), Times.Once);
            mock.Verify(c => c.GetUsersByUsername(username), Times.Once);
        }
        
        [Fact]
        public async Task Create_User_With_Already_Taken_Username()
        {
            // arguments
            var username = "GoodName123";
            // mocks
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.CreateLock(It.IsAny<string>(), It.IsAny<TimeSpan>()));
            mock.Setup(c => c.GetUsersByUsername(username)).ReturnsAsync(new SkinnyUserAccountEntry[]
            {
                new SkinnyUserAccountEntry()
                {
                    userId = 123,
                    username = username.ToLower(),
                }
            });
            // test
            var service = new UsersService(mock.Object);
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(async () =>
            {
                await service.CreateUser(username);
            });
            // verify
            mock.Verify(c => c.InsertUser(username), Times.Never);
        }

        [Fact]
        public async Task Set_Birth_Date_With_Valid_Args_And_Existing_Entry()
        {
            var userId = 1;
            var newBirthDate = new DateTime(2000, 1, 3);
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.UpdateUserBirthDate(userId, newBirthDate));
            mock.Setup(c => c.DoesHaveAccountInformationEntry(userId)).ReturnsAsync(true);
            var service = new UsersService(mock.Object);
            await service.SetUserBirthDate(userId, newBirthDate);
            mock.Verify(c => c.UpdateUserBirthDate(userId, newBirthDate), Times.Once);
            mock.Verify(c => c.InsertAccountInformationEntry(It.IsAny<AccountInformationEntry>()), Times.Never);
        }
        
        [Fact]
        public async Task Set_Birth_Date_With_Valid_Args_And_No_Existing_Entry()
        {
            var userId = 1;
            var newBirthDate = new DateTime(2000, 1, 3);
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.UpdateUserBirthDate(userId, newBirthDate));
            mock.Setup(c => c.DoesHaveAccountInformationEntry(userId)).ReturnsAsync(false);
            var service = new UsersService(mock.Object);
            await service.SetUserBirthDate(userId, newBirthDate);
            mock.Verify(c => c.UpdateUserBirthDate(userId, newBirthDate), Times.Never);
            mock.Verify(c => c.InsertAccountInformationEntry(It.IsAny<AccountInformationEntry>()), Times.Once);
        }

        [Fact]
        public async Task Delete_User_By_Id()
        {
            var userId = 123;
            var mock = new Mock<IUsersDatabase>();
            mock.Setup(c => c.DeleteUser(userId));
            var service = new UsersService(mock.Object);
            await service.DeleteUser(userId);
            mock.Verify(c => c.DeleteUser(userId), Times.Once);
        }

        [Fact]
        public async Task Get_User_By_Name()
        {
            var username = "hello";
            var mock = new Mock<IUsersDatabase>();
            var request = new SkinnyUserAccountEntry[]
            {
                new SkinnyUserAccountEntry()
                {
                    username = "hello",
                    userId = 123,
                },
            };
            mock.Setup(c => c.GetUsersByUsername(username)).ReturnsAsync(request);
            var service = new UsersService(mock.Object);
            var resp = await service.GetUserByUsername(username);
            Assert.Equal(123, resp.userId);
            Assert.Equal(username, resp.username);
        }
        
        [Fact]
        public async Task Get_User_By_Name_Not_Exists()
        {
            var username = "hello";
            var mock = new Mock<IUsersDatabase>();
            var request = new SkinnyUserAccountEntry[]
            {

            };
            mock.Setup(c => c.GetUsersByUsername(username)).ReturnsAsync(request);
            var service = new UsersService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.GetUserByUsername(username);
            });
        }
    }
}