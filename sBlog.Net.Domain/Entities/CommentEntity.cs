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
using System;
using System.ComponentModel.DataAnnotations;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Domain.Entities
{
    [Table("comments")]
    [MetadataType(typeof(CommentEntityMetaData))]
    public class CommentEntity
    {
        [Key]
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentUserFullName { get; set; }
        public string CommenterEmail { get; set; }
        public string CommenterSite { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentPostedDate { get; set; }
        public int CommentStatus { get; set; } /* 0 - approved, 1 - pending, 2 - spam, -1 - trash */
        public int? UserID { get; set; }
    }
}
