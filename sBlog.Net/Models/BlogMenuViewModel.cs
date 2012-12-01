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
using System.Collections.Generic;

namespace sBlog.Net.Models
{
    public class BlogMenuViewModel
    {
        public List<BlogMenuOption> Pages { get; set; }
    }

    public class BlogMenuOption
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Selected { get; set; }
    }
}