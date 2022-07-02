using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Tests.Tokens
{
    [TestClass]
    public class RefreshTokenFactoryTests
    {
        [TestMethod]
        public void GenerateRefreshToken_GivenValidInputs_ReturnsExpectedRefreshToken()
        {
            // Arrange
            var ipAddress = "192.168.100.1";

            // Act
            var refreshToken = new RefreshTokenFactory().GenerateRefreshToken(ipAddress);

            // Assert
            Assert.AreEqual(refreshToken.CreatedByIp, ipAddress);
        }
    }
}
