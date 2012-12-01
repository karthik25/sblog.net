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
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Models
{
    public class RssFeedViewModel
    {
        public string BlogTitle { get; set; }
        public string BlogCaption { get; set; }
        public string BlogUrl { get; set; }
        public string BlogLanguage { get; set; }
        public List<PostEntity> Posts { get; set; }
    }
}
