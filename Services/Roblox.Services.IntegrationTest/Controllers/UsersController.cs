using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Controllers;
using Roblox.Services.Database;
using Roblox.Services.Services;
using Xunit;

namespace Roblox.Services.IntegrationTest.Controllers
{
    public class IntegrationTestUsersController : IntegrationTestBase
    {
        [Fact]
        public async Task Get_User_Description_Exists()
        {
            var db = new UsersDatabase();
            var userId = 1;
            var desc = "This is an integration test description.";

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
    }
}