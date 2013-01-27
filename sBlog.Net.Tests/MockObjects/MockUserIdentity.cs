using System.Security.Principal;
using System.Web.Security;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockUserIdentity : IIdentity, IUserInfo
    {
        private FormsAuthenticationTicket _ticket;
        private readonly int _userId = 1;

        public MockUserIdentity(FormsAuthenticationTicket ticket, int userId)
        {
            _ticket = ticket;
            _userId = userId;
        }

        public string AuthenticationType
        {
            get { return "User"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return "TestName"; }
        }

        public string UserId
        {
            get { return _userId.ToString(); }
        }

        public string UserToken
        {
            get { return "TestToken"; }
        }
    }
}
