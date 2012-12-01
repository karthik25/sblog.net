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
    public class BlogPostPageViewModel
    {
        public bool NextPageValid { get; set; }
        public int NextPageNumber { get; set; }
        public bool PreviousPageValid { get; set; }
        public int PreviousPageNumber { get; set; }
        public int CurrentPageNumber { get; set; }
        public List<PostEntity> Posts { get; set; }
        public CategoryEntity Category { get; set; }
        public TagEntity Tag { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string BlogName { get; set; }
        public string BlogCaption { get; set; }

        public bool Any
        {
            get
            {
                return Posts != null && Posts.Count > 0;
            }
        }
    }
}
