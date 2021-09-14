using System;
using System.Threading.Tasks;
using Moq;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Passwords;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Services
{
    public class UnitTestPasswordsService
    {
        [Fact]
        public async Task Return_True_For_Valid_Password()
        {
            var userId = 1;
            var pass = "HelloWorld123";
            var expectedHash = "$2a$12$5k9fV8vtRjHz1Fn4LtqKU.eDfmo3Vs.GroKmTwvyansxE.DI3hkhK";
            
            var mock = new Mock<IPasswordsDatabase>();
            mock.Setup(c => c.GetPasswordEntryForUser(userId)).ReturnsAsync(new UserAccountPasswordEntry()
            {
                passwordHash = expectedHash,
                userId = userId,
            });
            var service = new PasswordsService(mock.Object);
            var result = await service.IsPasswordCorrectForUser(userId, pass);
            Assert.True(result);
            mock.Verify(c => c.GetPasswordEntryForUser(userId), Times.Once);
        }
        
        [Fact]
        public async Task Return_False_For_Invalid_Password()
        {
            var userId = 1;
            var pass = "DoesNotMatchHash123";
            var expectedHash = "$2a$12$5k9fV8vtRjHz1Fn4LtqKU.eDfmo3Vs.GroKmTwvyansxE.DI3hkhK";
            
            var mock = new Mock<IPasswordsDatabase>();
            mock.Setup(c => c.GetPasswordEntryForUser(userId)).ReturnsAsync(new UserAccountPasswordEntry()
            {
                passwordHash = expectedHash,
                userId = userId,
            });
            var service = new PasswordsService(mock.Object);
            var result = await service.IsPasswordCorrectForUser(userId, pass);
            Assert.False(result);
            mock.Verify(c => c.GetPasswordEntryForUser(userId), Times.Once);
        }
                
        [Fact]
        public async Task Throw_RecordNotFound_Due_To_Password_Not_Existing()
        {
            var userId = 1;
            var pass = "HelloWorld123";
            
            var mock = new Mock<IPasswordsDatabase>();
            mock.Setup(c => c.GetPasswordEntryForUser(userId)).ReturnsAsync((UserAccountPasswordEntry)null);
            var service = new PasswordsService(mock.Object);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await service.IsPasswordCorrectForUser(userId, pass);
            });
        }
                
        [Fact]
        public async Task Set_Password_For_User_With_Password()
        {
            var userId = 1;
            var pass = "HelloWorld123";
            var expectedHash = "$2a$12$5k9fV8vtRjHz1Fn4LtqKU.eDfmo3Vs.GroKmTwvyansxE.DI3hkhK";
            
            var mock = new Mock<IPasswordsDatabase>();
            mock.Setup(c => c.GetPasswordEntryForUser(userId)).ReturnsAsync(new UserAccountPasswordEntry()
            {
                passwordHash = expectedHash,
                userId = userId,
            });
            mock.Setup(c => c.SetPassword(userId, It.IsAny<string>()));
            var service = new PasswordsService(mock.Object);
            await service.SetPasswordForUser(userId, pass);
            mock.Verify(c => c.GetPasswordEntryForUser(userId), Times.Once);
            mock.Verify(c => c.SetPassword(userId, It.IsAny<string>()), Times.Once);
        }
                        
        [Fact]
        public async Task Set_Password_For_User_Without_Password()
        {
            var userId = 1;
            var pass = "HelloWorld123";
            
            var mock = new Mock<IPasswordsDatabase>();
            mock.Setup(c => c.GetPasswordEntryForUser(userId)).ReturnsAsync((UserAccountPasswordEntry) null);
            mock.Setup(c => c.InsertPassword(userId, It.IsAny<string>()));
            var service = new PasswordsService(mock.Object);
            await service.SetPasswordForUser(userId, pass);
            mock.Verify(c => c.GetPasswordEntryForUser(userId), Times.Once);
            mock.Verify(c => c.InsertPassword(userId, It.IsAny<string>()), Times.Once);
        }
    }
}