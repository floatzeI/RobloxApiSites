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
        public void Return_Date_On_Bad_Birth_Date()
        {
            var m = new CreateUserRequest
            {
                username = "User123",
                birthDay = 40,
                birthMonth = 1,
                birthYear = 1990
            };
            Assert.Equal("birthDay", m.Validate());
        }
    }
}