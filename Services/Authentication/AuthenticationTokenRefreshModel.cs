// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationTokenRefreshModel
    {
        public AuthenticationTokenRefreshModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}