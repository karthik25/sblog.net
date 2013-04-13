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
    [Table("errors")]
    public class ApplicationErrorEntity
    {
        [Key]
        public int ErrorID { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
    }
}
