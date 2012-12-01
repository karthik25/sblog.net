using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockUserIdentity : IIdentity, IUserInfo
    {
        private FormsAuthenticationTicket _ticket;

        private int _userID = 1;

        public MockUserIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
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
            get { return _ticket.Name; }
        }

        public string UserId
        {
            get { return _userID.ToString(); }
        }

        public string UserToken
        {
            get { return _ticket.UserData.Split(':').Last(); }
        }

        public void SetUserID(int userID)
        {
            _userID = userID;
        }
    }
}
