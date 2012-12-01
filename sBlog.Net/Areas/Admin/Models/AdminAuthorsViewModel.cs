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

namespace sBlog.Net.Areas.Admin.Models
{
    public class AdminAuthorsViewModel : AdminBaseViewModel
    {
        public List<UserEntity> Authors { get; set; }
        public int AuthorsCount { get; set; }
    }
}