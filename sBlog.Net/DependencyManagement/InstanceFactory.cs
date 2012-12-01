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
            var applicationFactory = GetFactory();
            return applicationFactory.CreateConcreteInstance<IUser>();
        }

        public static ISettings CreateSettingsInstance()
        {
            var applicationFactory = GetFactory();
            return applicationFactory.CreateConcreteInstance<ISettings>();
        }

        public static IError CreateErrorInstance()
        {
            var applicationFactory = GetFactory();
            return applicationFactory.CreateConcreteInstance<IError>();
        }

        private static NinjectControllerFactory GetFactory()
        {
            return (NinjectControllerFactory) ControllerBuilder.Current.GetControllerFactory();
        }
    }
}
