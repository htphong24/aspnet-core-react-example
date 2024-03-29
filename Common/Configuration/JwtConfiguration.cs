﻿namespace Common.Configuration
{
    public class JwtConfiguration
    {
        public string Key { get; set; }

        public double MinutesToExpiration { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}