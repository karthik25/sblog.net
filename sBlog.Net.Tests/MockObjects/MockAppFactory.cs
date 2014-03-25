using System.Security.Principal;
using Moq;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockObjects
{
    public static class MockAppFactory
    {
        public static IIdentity GetMockUserIdentity(int userId)
        {
            var iIdentity = new Mock<IIdentity>();
            iIdentity.SetupGet(x => x.IsAuthenticated).Returns(true);
            iIdentity.SetupGet(x => x.AuthenticationType).Returns("User");
            iIdentity.SetupGet(x => x.Name).Returns("TestName");

            var iUserInfo = iIdentity.As<IUserInfo>();
            iUserInfo.SetupGet(x => x.UserToken).Returns("TestToken");
            iUserInfo.SetupGet(x => x.UserId).Returns(userId.ToString());

            return iIdentity.Object;
        }

        public static IError GetMockError()
        {
            var error = new Mock<IError>();
            return error.Object;
        }
    }
}