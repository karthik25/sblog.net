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
using System.Web.Mvc;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.DependencyManagement
{
    public class InstanceFactory
    {
        public static IUser CreateUserInstance()
        {
            return DependencyResolver.Current.GetService<IUser>();
        }

        public static ISettings CreateSettingsInstance()
        {
            return DependencyResolver.Current.GetService<ISettings>();
        }

        public static IError CreateErrorInstance()
        {
            return DependencyResolver.Current.GetService<IError>();
        }
    }
}
