using Roblox.Authentication.Api.Controllers;
using Xunit;

namespace Roblox.Authentication.Api.UnitTest.Controllers.V1
{
    public class UnitTestRecoveryController
    {
        [Fact]
        public void Return_Metadata()
        {
            var controller = new RecoveryController();
            var result = controller.GetMetadata();
            Assert.NotNull(result);
            Assert.Equal(6, result.codeLength);
        }
    }
}