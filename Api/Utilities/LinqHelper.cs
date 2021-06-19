using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class LinqHelper
    {

        /// <summary>
        /// Does a list contain all values of another list?
        /// </summary>
        /// <remarks>Included in .Net 4 Enumerable.All </remarks>
        /// <typeparam name="T">list value type</typeparam>
        /// <param name="source">the larger list we're checking in</param>
        /// <param name="values">the list to look for in the containing list</param>
        /// <returns>true if it has everything</returns>
        public static bool ContainsAll<T>(IEnumerable<T> source, IEnumerable<T> values)
        {
            return values.All(value => source.Contains(value));
        }

     
      
    }
}
