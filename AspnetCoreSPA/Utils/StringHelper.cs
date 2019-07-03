using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utils
{
    public static class StringHelper
    {
        /// <summary>
        /// Gets substring of the specified value from start to maxLength
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Trim(this string value, int maxLength)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Substring(0, maxLength);
        }
    }
}
