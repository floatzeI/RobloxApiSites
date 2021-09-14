using System.Threading.Tasks;
using Moq;
using Roblox.Authentication.Api.Controllers;
using Roblox.Authentication.Api.Exceptions;
using Roblox.Passwords.Client;
using Roblox.Passwords.Client.Exceptions;
using Roblox.Platform.Authentication;
using Roblox.Platform.Membership;
using Roblox.Sessions.Client;
using Roblox.Users.Client;
using Roblox.Users.Client.Exceptions;
using Roblox.Users.Client.Models.Responses;
using Xunit;

namespace Roblox.Authentication.Api.UnitTest.Controllers.V1
{
    public class UnitTestAuthenticationController
    {
        [Fact]
        public async Task Get_Authentication_Metadata()
        {
            var controller = new AuthenticationController(null, null, null);
            var result = controller.GetMetaData();
            Assert.True(result.cookieLawNoticeTimeout > 1000);
        }

        [Fact]
        public async Task Login_To_Account()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            var userId = 1;
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ReturnsAsync(new SkinnyUserEntry()
            {
                username = username,
                userId = userId,
            });
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, password)).ReturnsAsync(true);
            usersClient.Setup(c => c.GetUserById(userId)).ReturnsAsync(new GetUserByIdEntry()
            {
                username = username,
                userId = userId,
                accountStatus = AccountStatus.Ok,
            });
            sessionsClient.Setup(c => c.CreateSession(userId)).ReturnsAsync("SessionCookieHere");
            
            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            var result = await controller.Login(new()
            {
                ctype = CredentialsType.Username,
                cvalue = username,
                password = password,
            });
            // assert
            Assert.Equal(result.user.id, userId);
            Assert.Equal(result.user.name, username);
            // verify
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, password), Times.Once);
            usersClient.Verify(c => c.GetUserById(userId), Times.Once);
            sessionsClient.Verify(c => c.CreateSession(userId), Times.Once);
        }
        
        [Fact]
        public async Task Login_To_Locked_Account_And_Fail()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            var userId = 1;
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ReturnsAsync(new SkinnyUserEntry()
            {
                username = username,
                userId = userId,
            });
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, password)).ReturnsAsync(true);
            usersClient.Setup(c => c.GetUserById(userId)).ReturnsAsync(new GetUserByIdEntry()
            {
                username = username,
                userId = userId,
                accountStatus = AccountStatus.MustValidateEmail,
            });
            
            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<LockedAccountException>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.Username,
                    cvalue = username,
                    password = password,
                });
            });
            // verify
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, password), Times.Once);
            usersClient.Verify(c => c.GetUserById(userId), Times.Once);
            sessionsClient.Verify(c => c.CreateSession(userId), Times.Never);
        }
        
        [Fact]
        public async Task Login_To_Gdpr_Account_And_Fail()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            var userId = 1;
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ReturnsAsync(new SkinnyUserEntry()
            {
                username = username,
                userId = userId,
            });
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, password)).ReturnsAsync(true);
            usersClient.Setup(c => c.GetUserById(userId)).ReturnsAsync(new GetUserByIdEntry()
            {
                username = username,
                userId = userId,
                accountStatus = AccountStatus.Forgotten,
            });
            
            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<IncorrectCredentialsException>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.Username,
                    cvalue = username,
                    password = password,
                });
            });
            // verify
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, password), Times.Once);
            usersClient.Verify(c => c.GetUserById(userId), Times.Once);
            sessionsClient.Verify(c => c.CreateSession(userId), Times.Never);
        }
        
         
        [Fact]
        public async Task Login_With_Invalid_Username_And_Fail()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ThrowsAsync(new UserNotFoundException());

            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<IncorrectCredentialsException>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.Username,
                    cvalue = username,
                    password = password,
                });
            });
        }
        
        [Fact]
        public async Task Login_With_Incorrect_Password_And_Fail()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            var userId = 1;
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ReturnsAsync(new SkinnyUserEntry()
            {
                username = username,
                userId = userId,
            });
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, password)).ReturnsAsync(false);
            usersClient.Setup(c => c.GetUserById(userId)).ReturnsAsync(new GetUserByIdEntry()
            {
                username = username,
                userId = userId,
                accountStatus = AccountStatus.Ok,
            });
            
            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<IncorrectCredentialsException>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.Username,
                    cvalue = username,
                    password = password,
                });
            });
            // verify
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, password), Times.Once);
            sessionsClient.Verify(c => c.CreateSession(userId), Times.Never);
        }
        
        [Fact]
        public async Task Login_To_Account_With_No_Password_And_Fail()
        {
            // arguments
            var username = "Roblox";
            var password = "Roblox123";
            var userId = 1;
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            usersClient.Setup(c => c.GetUserByUsername(username)).ReturnsAsync(new SkinnyUserEntry()
            {
                username = username,
                userId = userId,
            });
            passwordsClient.Setup(c => c.IsPasswordCorrect(userId, password))
                .ThrowsAsync(new UserHasNoPasswordException());
            usersClient.Setup(c => c.GetUserById(userId)).ReturnsAsync(new GetUserByIdEntry()
            {
                username = username,
                userId = userId,
                accountStatus = AccountStatus.Forgotten,
            });
            
            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<LoginAccountIssueException>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.Username,
                    cvalue = username,
                    password = password,
                });
            });
            // verify
            passwordsClient.Verify(c => c.IsPasswordCorrect(userId, password), Times.Once);
            sessionsClient.Verify(c => c.CreateSession(userId), Times.Never);
        }
                
        [Fact]
        public async Task Login_With_Unsupported_Method_And_Fail_Temporary()
        {
            // arguments
            var password = "Roblox123";
            
            // mocks
            var usersClient = new Mock<IUsersV1Client>();
            var passwordsClient = new Mock<IPasswordsV1Client>();
            var sessionsClient = new Mock<ISessionsV1Client>();

            // create and run
            var controller = new AuthenticationController(usersClient.Object, passwordsClient.Object, sessionsClient.Object);
            // Assert
            await Assert.ThrowsAsync<CredentialsNotSuitableForLogin>(async () =>
            {
                await controller.Login(new()
                {
                    ctype = CredentialsType.PhoneNumber,
                    cvalue = "18005555555",
                    password = password,
                });
            });
        }
    }
}