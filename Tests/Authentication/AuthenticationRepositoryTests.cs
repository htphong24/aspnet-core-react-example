using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Tests.Tokens
{
    [TestClass]
    public class AuthenticationRepositoryTests
    {
        private readonly SqlServerAuthenticationRepository _authRepo;
        private readonly Mock<ContactsMgmtContext> _contextMock = new Mock<ContactsMgmtContext>();
        private readonly Mock<ContactsMgmtIdentityContext> _idContextMock = new Mock<ContactsMgmtIdentityContext>();

        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private readonly Mock<UserManager<ApplicationUser>> _usrMgrMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInMgrMock;

        private readonly Mock<IJwtFactory> _jwtFactoryMock = new Mock<IJwtFactory>();
        private readonly Mock<IRefreshTokenFactory> _refreshTokenFactoryMock = new Mock<IRefreshTokenFactory>();

        public AuthenticationRepositoryTests()
        {
            _usrMgrMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _signInMgrMock = new Mock<SignInManager<ApplicationUser>>(_usrMgrMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null);

            _authRepo = new SqlServerAuthenticationRepository(
                _contextMock.Object,
                _idContextMock.Object,
                _mapperMock.Object,
                _usrMgrMock.Object,
                _signInMgrMock.Object,
                _jwtFactoryMock.Object,
                _refreshTokenFactoryMock.Object
            );
        }

        [TestMethod]
        public async Task LoginAsync_GivenValidCredentials_ShouldSucceed()
        {
            // Arrange
            var rq = new AuthenticationLoginRequest
            {
                Email = "test@abc.com",
                Password = "123",
                IpAddress = "0.0.0.1"
            };
            var user = new ApplicationUser()
            {
                RefreshTokens = new List<RefreshToken>()
            };
            _usrMgrMock.Setup(mgr => mgr.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _signInMgrMock.Setup(mgr => mgr.CheckPasswordSignInAsync(user, It.IsAny<string>(), true)).ReturnsAsync(SignInResult.Success);

            var roles = new List<string>();
            _usrMgrMock.Setup(mgr => mgr.GetRolesAsync(user)).ReturnsAsync(roles);

            string jwt = "a.b.c";
            _jwtFactoryMock.Setup(fac => fac.GenerateJwt(user, roles)).Returns(jwt);
            var refreshToken = new RefreshToken();
            _refreshTokenFactoryMock.Setup(fac => fac.GenerateRefreshToken(It.IsAny<string>())).Returns(refreshToken);
            _usrMgrMock.Setup(mgr => mgr.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var rs = await _authRepo.LoginAsync(rq);

            // Assert
            Assert.IsTrue(rs.Success);
            Assert.AreEqual(rs.AccessToken, jwt);
            Assert.AreEqual(rs.RefreshToken, refreshToken.Token);
        }
    }
}
