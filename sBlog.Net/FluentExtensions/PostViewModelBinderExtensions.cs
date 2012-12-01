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
using System.Web.Mvc;

namespace sBlog.Net.FluentExtensions
{
    public static class PostViewModelBinderExtensions
    {
        public static string GetValue(this ModelBindingContext bindingContext, string keyName)
        {
            if (bindingContext == null)
                throw new NullReferenceException("Binding context is null");

            var values = (string[])bindingContext.ValueProvider.GetValue(keyName).RawValue;
            return values.First();
        }
    }
}
