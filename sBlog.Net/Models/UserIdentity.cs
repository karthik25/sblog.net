#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
using System;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Runtime.Serialization;
using sBlog.Net.Domain.Interfaces;
using System.Web.Security;

namespace sBlog.Net.Models
{
    [Serializable]
    public class UserIdentity : IIdentity, IUserInfo, ISerializable, IPrincipal
    {
        private readonly FormsAuthenticationTicket _ticket;

        public UserIdentity(FormsAuthenticationTicket ticket)
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
            get { return _ticket.UserData.Split(':').First(); }
        }

        public string UserToken
        {
            get { return _ticket.UserData.Split(':').Last(); }
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (context.State != StreamingContextStates.CrossAppDomain)
            {
                throw new InvalidOperationException("Serialization not supported");
            }

            var gIdent = new GenericIdentity(Name, AuthenticationType);
            info.SetType(gIdent.GetType());

            var serializableMembers = FormatterServices.GetSerializableMembers(gIdent.GetType());
            var serializableValues = FormatterServices.GetObjectData(gIdent, serializableMembers);

            for (var i = 0; i < serializableMembers.Length; i++)
            {
                info.AddValue(serializableMembers[i].Name, serializableValues[i]);
            }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(Name);
        }

        public IIdentity Identity
        {
            get { return this; }
        }
    }
}
