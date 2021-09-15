using System;
using System.Collections.Generic;

namespace Roblox.Services.Lib.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Get items that appear in the first list, but not in the second
        /// </summary>
        /// <param name="first">The first list of T</param>
        /// <param name="second">The second list of T</param>
        /// <param name="comparison">A lambda that returns whether both items are equal (true) or not (false)</param>
        /// <typeparam name="T">The type of the items to compare</typeparam>
        /// <returns>A new list containing items that do not appear in the second list. Length is 0 if all items appear</returns>
        public static List<T> GetItemsNotInSecondList<T>(List<T> first, List<T> second, Func<T, T, bool> comparison)
        {
            var notInSecondList = new List<T>();
            foreach (var item in first)
            {
                // List<T>.Find() would be nice, but I don't want to force nullable reference types/use default(T) for comparisons
                var existsInSecondList = false;
                foreach (var otherItem in second)
                {
                    if (!comparison(item, otherItem)) continue;
                    existsInSecondList = true;
                    break;
                }
                if (!existsInSecondList)
                {
                    notInSecondList.Add(item);
                }
            }

            return notInSecondList;
        }
    }
}