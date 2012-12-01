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
using System.Globalization;
using sBlog.Net.CustomExceptions;

namespace sBlog.Net.FluentExtensions
{
    public static class StringUrlExtensions
    {
        public static string GetMonthName(this string monthCode)
        {
            int monthNumber;
            if (!int.TryParse(monthCode, out  monthNumber) || monthNumber < 1 || monthNumber > 12)
            {
                throw new InvalidMonthException("An invalid month number was passed", monthCode);
            }

            var formatInfo = new DateTimeFormatInfo();
            return formatInfo.GetMonthName(monthNumber).ToLower();
        }

        public static string GetValidUrl(this string srcUrl)
        {
            if (string.IsNullOrEmpty(srcUrl))
                return "javascript:void(0)";

            if (srcUrl.StartsWith("http://") || srcUrl.StartsWith("https://"))
                return srcUrl;

            return string.Format("http://{0}", srcUrl);
        }
    }
}
