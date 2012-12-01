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
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Domain.Entities
{
    [Table(Name = "Comments")]
    [MetadataType(typeof(CommentEntityMetaData))]
    public class CommentEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int CommentID { get; set; }
        [Column] public int PostID { get; set; }
        [Column] public string CommentUserFullName { get; set; }
        [Column] public string CommenterEmail { get; set; }
        [Column] public string CommenterSite { get; set; }
        [Column] public string CommentContent { get; set; }
        [Column] public DateTime CommentPostedDate { get; set; }
        [Column] public int CommentStatus { get; set; } /* 0 - approved, 1 - pending, 2 - spam, -1 - trash */
        [Column] public int? UserID { get; set; }
    }
}
