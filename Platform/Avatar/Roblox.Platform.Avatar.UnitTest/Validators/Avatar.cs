using System.Collections.Generic;
using Roblox.Web.Enums;
using Xunit;

namespace Roblox.Platform.Avatar.UnitTest
{
    public class UnitTestAvatarValidator
    {
        [Fact]
        public void Return_True_For_Valid_Increment()
        {
            var increments = new List<decimal>()
            {
                1.0m,
                0.91m,
                0.92m,
                0.99m,
            };
            foreach (var userValue in increments)
            {
                var result = AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.head, userValue);
                Assert.True(result, "Result failed for " + userValue);
            }
        }
        
        [Fact]
        public void Return_False_For_Invalid_Increment()
        {
            var increments = new List<decimal>()
            {
                1.009m,
                1.100042m,
                0.9993m,
                0.8153m,
                0.825m,
                0.80001m,
            };
            foreach (var userValue in increments)
            {
                var result = AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.head, userValue);
                Assert.False(result);
            }
        }
        
        [Fact]
        public void Return_False_For_WrongSize_Increment()
        {
            var increments = new List<decimal>()
            {
                0.8m,
                1.1m,
                100m,
                1.4m,
            };
            foreach (var userValue in increments)
            {
                var result = AvatarValidator.IsScaleValid(AvatarValidator.scaleRules.head, userValue);
                Assert.False(result);
            }
        }
    }
}