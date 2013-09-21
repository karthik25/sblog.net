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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Domain.Entities
{
    [Table(Name = "Posts")]
    [MetadataType(typeof(PostEntityMetaData))]
    public class PostEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int PostID { get; set; }
        [Column]
        public string PostTitle { get; set; }
        [Column] public string PostUrl { get; set; }
        [Column] public string PostContent { get; set; }
        [Column] public DateTime PostAddedDate { get; set; }
        [Column] public DateTime? PostEditedDate { get; set; }
        [Column] public int OwnerUserID { get; set; }
        
        [Column]
        [DisplayName("user can add comments?")]
        public bool UserCanAddComments { get; set; }

        [Column] 
        [DisplayName("provide sharing options")]
        public bool CanBeShared { get; set; }

        [Column]
        [DisplayName("mark as private")]
        public bool IsPrivate { get; set; }

        [Column] public byte EntryType { get; set; } /* 1 - Post, 2 - Page */

        [Column]
        public int? Order { get; set; }

        [Column]
        public string BitlyUrl { get; set; }

        [Column]
        public string BitlySourceUrl { get; set; }

        #region Additional Properties
        /* 
         * Properties to help associate other things associated to a post/page 
         * 
         * In future, I will also try to free this class of this responsibility,
         * if required.
         * 
         * Appropriate file would be PostModel.cs
         * 
         */
        public string OwnerUserName { get; set; }
        public string UserName { get; set; }
        public List<CategoryEntity> Categories { get; set; }
        public List<TagEntity> Tags { get; set; }
        public List<CommentEntity> Comments { get; set; }

        /* Properties that are independent of the database content */
        public string ItemType
        {
            get { return EntryType == 1 ? "post" : "page"; }
        }

        public string PostYear
        {
            get
            {
                return PostAddedDate.Year.ToString();
            }
        }

        public string PostMonth
        {
            get
            {
                return PostAddedDate.Month.ToString("00");
            }
        }
        #endregion
    }
}
