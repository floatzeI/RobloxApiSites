using System;
using Roblox.Services.Models.Users;
using Xunit;

namespace Roblox.Services.UnitTest.Models.Users
{
    public class UnitTestCreateUserRequest
    {
        
        [Fact]
        public void Return_Null_On_Good_Validate()
        {
            var m = new CreateUserRequest
            {
                username = "GoodUsername123",
                birthDay = 10,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Null(m.Validate());
        }
        
        [Fact]
        public void Return_Username_On_Short_Name()
        {
            var m = new CreateUserRequest
            {
                username = "a",
                birthDay = 10,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("username", m.Validate());
        }
        
        [Fact]
        public void Return_Username_On_Long_Name()
        {
            var m = new CreateUserRequest
            {
                username = "RobloxRobloxRobloxRobloxRobloxRobloxRobloxRobloxRobloxRobloxRobloxRoblox",
                birthDay = 10,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("username", m.Validate());
        }
        
        [Fact]
        public void Return_Username_On_Null_Name()
        {
            var m = new CreateUserRequest
            {
                username = null,
                birthDay = 10,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("username", m.Validate());
        }
        
        [Fact]
        public void Return_Username_On_Empty_Name()
        {
            var m = new CreateUserRequest
            {
                username = "",
                birthDay = 10,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("username", m.Validate());
        }
                
        [Fact]
        public void Return_BirthDay_On_Negative_Day()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = -1,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("birthDay", m.Validate());
        }
                        
        [Fact]
        public void Return_BirthDay_On_Too_Big_Day()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = 34,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("birthDay", m.Validate());
        }
        
        [Fact]
        public void Return_BirthMonth_On_Negative_Month()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = 10,
                birthMonth = -1,
                birthYear = 1990
            };
            Assert.Equal("birthMonth", m.Validate());
        }
        
        [Fact]
        public void Return_BirthMonth_On_Too_Big_Month()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = 10,
                birthMonth = 15,
                birthYear = 1990
            };
            Assert.Equal("birthMonth", m.Validate());
        }
                
        [Fact]
        public void Return_BirthYear_On_Too_Small_Year()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = 10,
                birthMonth = 10,
                birthYear = 1900
            };
            Assert.Equal("birthYear", m.Validate());
        }
                        
        [Fact]
        public void Return_BirthYear_On_Too_Big_Year()
        {
            var m = new CreateUserRequest
            {
                username = "Username123",
                birthDay = 10,
                birthMonth = 10,
                birthYear = DateTime.Now.Year + 1,
            };
            Assert.Equal("birthYear", m.Validate());
        }
    }
}