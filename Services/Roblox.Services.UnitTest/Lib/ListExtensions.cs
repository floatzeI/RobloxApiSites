using System.Collections.Generic;
using System.Linq;
using Roblox.Services.Lib.Extensions;
using Roblox.Services.Models;
using Roblox.Services.Models.Shared;
using Xunit;

namespace Roblox.Services.UnitTest.Lib
{
    public class TestListExtensions
    {
        [Fact]
        public void Get_Longs_Not_In_Second_List()
        {
            var firstList = new List<long>()
            {
                1,
                2,
                3,
                4,
            };
            var secondList = new List<long>()
            {
                1,
                2,
                4,
            };
            var expectedResult = new List<long>()
            {
                3,
            };

            var result = ListExtensions.GetItemsNotInSecondList(firstList, secondList, (l, l1) => l == l1);
            Assert.Equal(expectedResult.Count, result.Count);
            for (var i = 0; i < result.Count; i++)
            {
                var sibling = expectedResult[i];
                Assert.Equal(sibling, result[i]);
            }
        }
        
        [Fact]
        public void Get_Classes_Not_In_Second_List()
        {
            var firstList = new List<Roblox.Services.Models.Shared.HealthCheckResponse>()
            {
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing1.Service",
                },
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing2.Service",
                },
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing3.Service",
                },
            };
            var secondList = new List<HealthCheckResponse>()
            {
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing1.Service",
                },
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing2.Service",
                },
            };
            var expectedResult = new List<HealthCheckResponse>()
            {
                new ()
                {
                    status = "OK",
                    name = "Roblox.Testing3.Service",
                },
            };

            var result = ListExtensions.GetItemsNotInSecondList(firstList, secondList, (l, l1) => l.name == l1.name && l.status == l1.status);
            Assert.Equal(expectedResult.Count, result.Count);
            for (var i = 0; i < result.Count; i++)
            {
                var sibling = expectedResult[i];
                Assert.Equal(sibling.name, result[i].name);
                Assert.Equal(sibling.status, result[i].status);
            }
        }
        
        [Fact]
        public void Get_Empty_Long_List()
        {
            var firstList = new List<long>()
            {
                1,
                2,
                3,
                4,
            };

            var result = ListExtensions.GetItemsNotInSecondList(firstList, firstList, (l, l1) => l == l1);
            Assert.Empty(result);
        }

        [Fact]
        public void Convert_Csv_Of_Ints_To_Long_List()
        {
            var param = "1,2,3";
            var expected = new List<long>()
            {
                1, 2, 3
            };

            var result = ListExtensions.CsvToLongList(param);
            
            Assert.Equal(expected.Count, expected.Count);
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }
        
        [Fact]
        public void Convert_Csv_Of_Ints_To_Long_List_With_Invalid_Params()
        {
            var param = "1,2,badstringshouldbeignored,3";
            var expected = new List<long>()
            {
                1, 2, 3
            };

            var result = ListExtensions.CsvToLongList(param);
            
            Assert.Equal(expected.Count, expected.Count);
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }
    }
}