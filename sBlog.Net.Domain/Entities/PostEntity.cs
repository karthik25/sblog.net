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
using System.ComponentModel;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Domain.Entities
{
    [Table("posts")]
    [MetadataType(typeof(PostEntityMetaData))]
    public class PostEntity
    {
        [Key]
        public int PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostUrl { get; set; }
        public string PostContent { get; set; }
        public DateTime PostAddedDate { get; set; }
        public DateTime? PostEditedDate { get; set; }
        public int OwnerUserID { get; set; }
        
        [DisplayName("user can add comments?")]
        public bool UserCanAddComments { get; set; }

        [DisplayName("provide sharing options")]
        public bool CanBeShared { get; set; }

        [DisplayName("mark as private")]
        public bool IsPrivate { get; set; }

        public byte EntryType { get; set; } /* 1 - Post, 2 - Page */

        public int? Order { get; set; }

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
        [NotMapped]
        public string OwnerUserName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public List<CategoryEntity> Categories { get; set; }
        [NotMapped]
        public List<TagEntity> Tags { get; set; }
        [NotMapped]
        public List<CommentEntity> Comments { get; set; }

        /* Properties that are independent of the database content */
        [NotMapped]
        public string ItemType
        {
            get { return EntryType == 1 ? "post" : "page"; }
        }

        [NotMapped]
        public string PostYear
        {
            get
            {
                return PostAddedDate.Year.ToString();
            }
        }

        [NotMapped]
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
