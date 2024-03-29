﻿using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationLogoutRequest : RequestBase
    {
        public AuthenticationLogoutRequest()
            : base()
        {
        }

        public string Email { get; set; }

        public string RefreshToken { get; set; } = null;

        public string IpAddress { get; set; } = null;
    }
}