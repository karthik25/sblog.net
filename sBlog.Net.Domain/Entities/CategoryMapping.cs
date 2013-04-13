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

namespace sBlog.Net.Domain.Entities
{
    [Table("categorymapping")]
    public class CategoryMapping
    {
        [Key]
        public int PostCategoryMappingID { get; set; }
        public int CategoryID { get; set; }
        public int PostID { get; set; }
    }
}
