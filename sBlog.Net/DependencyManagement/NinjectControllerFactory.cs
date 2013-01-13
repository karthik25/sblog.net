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
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Concrete;
using sBlog.Net.Mappers;
using sBlog.Net.Services;
using System.Net;
using System.Web.Routing;

namespace sBlog.Net.DependencyManagement
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel = new StandardKernel(new ApplicationIocServices());

        /// <summary>
        /// Gets a reference to the kernel, so that it could be shared with the dependency resolver
        /// </summary>
        /// <returns></returns>
        public IKernel GetKernel()
        {
            return _kernel;
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param><param name="controllerType">The type of the controller.</param><exception cref="T:System.Web.HttpException"><paramref name="controllerType"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="controllerType"/> cannot be assigned.</exception><exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                try
                {
                    var defaultController = base.GetControllerInstance(requestContext, null);
                    return defaultController;
                }
                catch (HttpException httpException)
                {
                    if (httpException.GetHttpCode() == (int) HttpStatusCode.NotFound)
                        throw new UrlNotFoundException("Unable to find a controller");
                    throw;
                }
            }

            return (IController)_kernel.Get(controllerType);
        }

        private class ApplicationIocServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IUser>().To<User>();
                Bind<IPost>().To<Post>();
                Bind<IComment>().To<Comment>();
                Bind<ICategory>().To<Category>();
                Bind<ITag>().To<Tag>();
                Bind<ISettings>().To<Settings>();
                Bind<IPathMapper>().To<PathMapper>();
                Bind<IError>().To<Error>();
                Bind<ICacheService>().To<CacheService>();
            }
        }
    }
}
