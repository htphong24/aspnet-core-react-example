using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utils
{
    public static class MiscHelper
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
                : value.Length <= maxLength 
                    ? value
                    : value.Substring(0, maxLength);
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
