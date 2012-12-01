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
    public class AdminPostOrPageViewModel : AdminBaseViewModel
    {
        public List<PostEntity> Posts { get; set; }
        public int AllPostsCount { get; set; }
        public int PrivatePostsCount { get; set; }
        public string Type { get; set; }
        public int PublicPostsCount
        {
            get
            {
                return AllPostsCount - PrivatePostsCount;
            }
        }
    }
}