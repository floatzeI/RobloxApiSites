using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Controllers;
using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers
{
    public class IntegrationTestUsersController : IntegrationTestBase
    {
        [Fact]
        public async Task Get_User_Description_Exists()
        {
            var db = new UsersDatabase(new DatabaseConfiguration<IUsersDatabaseCache>(new PostgresDatabaseProvider(), new UsersDatabaseCache()));
            var userId = 1;
            var desc = "This is an integration test description.";
            
            var defaultCacheProvider = new UsersDatabaseCache();
            await defaultCacheProvider.DeleteAccountInformation(userId);

            var exists = await Roblox.Services.Db.client.ExecuteAsync(
                "UPDATE account_information SET description = @description WHERE user_id = 1", new { user_id = userId, description = desc });
            if (exists == 0)
            {
                await db.InsertAccountInformationEntry(new()
                {
                    userId = userId,
                    description = desc
                });
            }
            var controller = new UsersController(new UsersService(db));
            var result = await controller.GetUserDescription(userId);
            Assert.Equal(userId, result.userId);
            Assert.Equal(desc, result.description);
        }
        
        [Fact]
        public async Task Get_User_Description_Not_Exists()
        {
            var db = new UsersDatabase(new DatabaseConfiguration<IUsersDatabaseCache>(new PostgresDatabaseProvider(), new UsersDatabaseCache()));
            var userId = 1;
            var defaultCacheProvider = new UsersDatabaseCache();
            await defaultCacheProvider.DeleteAccountInformation(userId);

            await Db.client.ExecuteAsync("DELETE FROM account_information WHERE user_id = @user_id", new
            {
                user_id = userId,
            });
            var controller = new UsersController(new UsersService(db));
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetUserDescription(userId);
            });
        }
        
        [Fact]
        public async Task Set_User_Description_Not_Exists()
        {
            var db = new UsersDatabase(new DatabaseConfiguration<IUsersDatabaseCache>(new PostgresDatabaseProvider(), new UsersDatabaseCache()));
            var userId = 1;
            var defaultCacheProvider = new UsersDatabaseCache();
            await defaultCacheProvider.DeleteAccountInformation(userId);
            var newDesc = "Integration test description 881BF1EB-A665-47BE-982F-6CE677932CD7";

            await Db.client.ExecuteAsync("DELETE FROM account_information WHERE user_id = @user_id", new
            {
                user_id = userId,
            });
            var controller = new UsersController(new UsersService(db));
            await controller.SetUserDescription(new SetDescriptionRequest()
            {
                userId = userId,
                description = newDesc,
            });

            var latestDesc = await controller.GetUserDescription(userId);
            Assert.Equal(userId, latestDesc.userId);
            Assert.Equal(newDesc, latestDesc.description);
        }
        
        [Fact]
        public async Task Set_User_Description_Exists()
        {
            var db = new UsersDatabase(new DatabaseConfiguration<IUsersDatabaseCache>(new PostgresDatabaseProvider(), new UsersDatabaseCache()));
            var userId = 1;
            var defaultCacheProvider = new UsersDatabaseCache();
            await defaultCacheProvider.DeleteAccountInformation(userId);
            var newDesc = "Integration test description 913D7A63-A769-4319-977A-87FDFFE70903";
            
            var controller = new UsersController(new UsersService(db));
            // this might insert or update depending on order of previous tests, so do it twice
            await controller.SetUserDescription(new SetDescriptionRequest()
            {
                userId = userId,
                description = newDesc,
            });
            
            await controller.SetUserDescription(new SetDescriptionRequest()
            {
                userId = userId,
                description = newDesc,
            });

            var latestDesc = await controller.GetUserDescription(userId);
            Assert.Equal(userId, latestDesc.userId);
            Assert.Equal(newDesc, latestDesc.description);
        }

        [Fact]
        public async Task Create_Random_User()
        {
            var username = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            var controller = new UsersController(new UsersService(new UsersDatabase(new(new PostgresDatabaseProvider(), new UsersDatabaseCache()))));
            var result = await controller.CreateUser(new()
            {
                username = username,
                birthYear = 2000,
                birthDay = 20,
                birthMonth = 4,
            });
            Assert.Equal(username, result.username);
            // todo: check birthdate
        }
        
        [Fact]
        public async Task Create_Random_User_And_Delete()
        {
            var username = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            var controller = new UsersController(new UsersService(new UsersDatabase(new(new PostgresDatabaseProvider(), new UsersDatabaseCache()))));
            var result = await controller.CreateUser(new()
            {
                username = username,
                birthYear = 2000,
                birthDay = 20,
                birthMonth = 4,
            });
            Assert.Equal(username, result.username);

            await controller.DeleteUser(result.userId);
            await Assert.ThrowsAsync<RecordNotFoundException>(async () =>
            {
                await controller.GetUserById(result.userId);
            });
        }
                
        [Fact]
        public async Task Create_Random_User_And_Set_Birth_Date()
        {
            var username = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            var controller = new UsersController(new UsersService(new UsersDatabase(new(new PostgresDatabaseProvider(), new UsersDatabaseCache()))));
            var result = await controller.CreateUser(new()
            {
                username = username,
                birthYear = 2000,
                birthDay = 20,
                birthMonth = 4,
            });
            Assert.Equal(username, result.username);

            // await controller;
            // todo
        }
    }
}