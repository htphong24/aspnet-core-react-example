using SqlServerDataAccess.EF;

namespace Services
{
    public interface IRefreshTokenFactory
    {
        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}