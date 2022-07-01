using SqlServerDataAccess.EF;
using System.Collections.Generic;

namespace Services
{
    public interface IJwtFactory
    {
        string GenerateJwt(ApplicationUser user, IList<string> roles);
    }
}