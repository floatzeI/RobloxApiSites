using Xunit;

namespace Roblox.Services.UnitTest.Validators
{
    public class UnitTestUsersValidator
    {
        [Fact]
        public void Return_Null_For_Valid_BirthDate()
        {
            Assert.Null(Roblox.Services.Validators.Users.ValidateBirthDate(2000, 4, 20));
        }
        
        [Fact]
        public void Return_BirthDay_On_Negative_Day()
        {
            Assert.Equal("birthDay", Roblox.Services.Validators.Users.ValidateBirthDate(2000, 1, -1));
        }
                        
        [Fact]
        public void Return_BirthDay_On_Too_Big_Day()
        {
            Assert.Equal("birthDay", Roblox.Services.Validators.Users.ValidateBirthDate(2000, 1, 100));
        }
        
        [Fact]
        public void Return_BirthMonth_On_Negative_Month()
        {
            Assert.Equal("birthMonth", Roblox.Services.Validators.Users.ValidateBirthDate(2000, -1, 1));
        }
        
        [Fact]
        public void Return_BirthMonth_On_Too_Big_Month()
        {
            Assert.Equal("birthMonth", Roblox.Services.Validators.Users.ValidateBirthDate(2000, 20, 1));
        }
                
        [Fact]
        public void Return_BirthYear_On_Too_Small_Year()
        {
            Assert.Equal("birthYear", Roblox.Services.Validators.Users.ValidateBirthDate(100, 1, 1));
        }
                        
        [Fact]
        public void Return_BirthYear_On_Too_Big_Year()
        {
            Assert.Equal("birthYear", Roblox.Services.Validators.Users.ValidateBirthDate(10000, 1, 1));
        }

        [Fact]
        public void Return_Null_For_Valid_Name()
        {
            Assert.Null(Roblox.Services.Validators.Users.ValidateUsername("GoodUsername123"));
        }
        
        [Fact]
        public void Return_Username_For_Too_Short_Name()
        {
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("a"));
        }
                
        [Fact]
        public void Return_Username_For_Too_Long_Name()
        {
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("RobloxRobloxRobloxRobloxRobloxRoblox"));
        }
                        
        [Fact]
        public void Return_Username_For_Name_With_Bad_Characters()
        {
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("Spaced Name 123"));
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("Period.Name.123"));
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("汉字"));
        }
        
        [Fact]
        public void Return_Username_For_Name_With_Bad_Start_Or_End_Characters()
        {
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("_Username123"));
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("Username123_"));
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("Use_rname123_"));
        }

        [Fact] 
        public void Return_Username_For_Username_With_Multiple_Forbidden_Characters()
        {
            Assert.Equal("username", Roblox.Services.Validators.Users.ValidateUsername("Use_rname_123"));
        }
    }
}