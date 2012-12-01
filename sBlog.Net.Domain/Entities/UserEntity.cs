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

namespace sBlog.Net.Domain.Entities
{
    [Table(Name="Users")]
    public class UserEntity
    {
        [Column(IsPrimaryKey=true, IsDbGenerated = true, AutoSync=AutoSync.OnInsert)]
        public int UserID { get; set; }
        [Column] public string UserName { get; set; }
        [Column] public string Password { get; set; }
        [Column] public string UserEmailAddress { get; set; }
        [Column] public string UserDisplayName { get; set; }
        [Column] public int? UserActiveStatus { get; set; }
        [Column] public string ActivationKey { get; set; }
        [Column] public string OneTimeToken { get; set; }
        [Column] public string UserCode { get; set; }
        [Column] public string UserSite { get; set; }
        [Column] public DateTime? LastLoginDate { get; set; }

        #region Additional Properties
        /* Properties that are independent of the database content */
        public string UserActiveStatusString
        {
            get
            {
                if (!UserActiveStatus.HasValue)
                    return "Yet to register";

                switch (UserActiveStatus.Value)
                {
                    case 0:
                        return "Inactive";
                    case 1:
                        return "Active";
                    default:
                        return "Unknown";
                }
            }
        }

        /* 
         * Properties to help associate other things associated to a user
         * 
         * In future, I will also try to free this class of this responsibility,
         * if required.
         * 
         */
        public int PostsCount { get; set; }
        #endregion
    }
}
