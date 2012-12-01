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
using System.Web;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Mappers
{
    public class PathMapper : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            return HttpContext.Current
                              .Server
                              .MapPath(relativePath);
        }
    }
}
