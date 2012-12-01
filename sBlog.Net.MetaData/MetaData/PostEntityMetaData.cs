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
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.MetaData.MetaData
{
    public class PostEntityMetaData
    {
        [Required(ErrorMessage="Post title field is required")]
        [StringLength(255, ErrorMessage = "Post title cannot be more than 255 characters")]
        public object PostTitle { get; set; }
    }
}
