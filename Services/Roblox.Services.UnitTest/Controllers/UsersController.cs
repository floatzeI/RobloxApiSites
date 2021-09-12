﻿using System;
using System.Threading.Tasks;
using Roblox.Services.Controllers;
using Roblox.Services.UnitTest.Controllers.Services;
using Xunit;

namespace Roblox.Services.UnitTest.Controllers
{
    public class UnitTestUsersController
    {
        private static UsersServiceMock service = new();
        private static UsersController controller = new(service);
        
        [Fact]
        public async Task Return_Mock_User_Description()
        {
            var userId = 1;
            var expectedDescription = "This is an example description for a unit test.";
            service.getDescriptionMockData = new()
            {
                userId = userId,
                created = DateTime.Now,
                updated = DateTime.Now,
                description = expectedDescription
            };
            var result = await controller.GetUserDescription(1);
            Assert.Equal(expectedDescription, result.description);
            Assert.Equal(userId, result.userId);
        }
    }
}