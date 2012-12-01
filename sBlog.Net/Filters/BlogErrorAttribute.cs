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
using sBlog.Net.Controllers;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Infrastructure;
using System.Web.Routing;

namespace sBlog.Net.Filters
{
    public class BlogErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Called when an exception occurs.
        /// 
        /// Depending on the type of exception, user is redirected to a corresponding page
        /// 
        ///     * UrlNotFoundException -> 404 page
        ///     * Any other exception -> a generic error page
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnException(ExceptionContext filterContext)
        {
            var errType = string.Empty;
            var exception = filterContext.Exception;

            var errorLogger = new ErrorLogger(exception);
            errorLogger.Log();

            filterContext.ExceptionHandled = true;
            filterContext.Result = GetRedirectResultByExceptionType(errType, filterContext);

            base.OnException(filterContext);
        }

        private static ActionResult GetRedirectResultByExceptionType(string errType, ExceptionContext exceptionContext)
        {
            var urlHelper = new UrlHelper(exceptionContext.HttpContext.Request.RequestContext);
            var exception = exceptionContext.Exception;
            var isAdminController = ((IControllerProperties) exceptionContext.Controller).IsAdminController;

            if (exception is UnauthorizedAccessException)
                errType = "unauthorized";
            else if (exception is InvalidMonthException)
                errType = "invalid-month";

            ActionResult redirectTo = isAdminController ? new RedirectResult(urlHelper.Action("Error","Admin", new { Area = "Admin", err = errType })) 
                                                        : new RedirectResult(urlHelper.Action("Error", "Home", new { Area = "", err = errType }));
            
            if (exception is UrlNotFoundException)
                redirectTo = new RedirectToRouteResult("Error404", new RouteValueDictionary());
            
            if (exception is InvalidThemeException)
                redirectTo = new RedirectToRouteResult("InvalidTheme", new RouteValueDictionary());

            return redirectTo;
        }
    }
}
