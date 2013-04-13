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

namespace sBlog.Net.Domain.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserDisplayName { get; set; }
        public int? UserActiveStatus { get; set; }
        public string ActivationKey { get; set; }
        public string OneTimeToken { get; set; }
        public string UserCode { get; set; }
        public string UserSite { get; set; }
        public DateTime? LastLoginDate { get; set; }

        #region Additional Properties
        /* Properties that are independent of the database content */
        [NotMapped]
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
        [NotMapped]
        public int PostsCount { get; set; }
        #endregion
    }
}
