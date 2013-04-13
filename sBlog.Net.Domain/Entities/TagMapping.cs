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
    [Table("tagmapping")]
    public class TagMapping
    {
        [Key]
        public int PostTagMappingID { get; set; }
        public int TagID { get; set; }
        public int PostID { get; set; }
    }
}
