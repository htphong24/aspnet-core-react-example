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
    public class JwtFactoryTests
    {
        [TestMethod]
        public void GenerateJwt_GivenValidInputs_ReturnsExpectedJwt()
        {
            // Arrange
            var newId = Guid.NewGuid().ToString();
            var user = new ApplicationUser
            {
                AccessFailedCount = 1,
                Email = "test@abc.com",
                Id = newId,
                UserName = "test",
                NormalizedUserName = "test",
            };
            var roles = new List<string>
            {
                "Standard"
            };
            var jwtConfig = new Common.Configuration.JwtConfiguration
            {
                Issuer = "Test",
                Audience = "Test",
                Key = "TestTestSecretKey",
                MinutesToExpiration = 1
            };
            var jwtFactory = new JwtFactory(Options.Create(jwtConfig));

            // Act
            var serializedJwt = jwtFactory.GenerateJwt(user, roles);
            var deserializedJwt = new JwtSecurityTokenHandler().ReadJwtToken(serializedJwt);

            // Assert
            Assert.AreEqual(deserializedJwt.Subject, newId);
        }
    }
}
