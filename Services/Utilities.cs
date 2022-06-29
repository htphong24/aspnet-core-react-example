using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public static class Utilities
    {
        public static string GetIdentityErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join('\n', errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}"));
        }
    }
}
