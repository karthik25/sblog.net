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
using System.Data.Linq.Mapping;

namespace sBlog.Net.Domain.Entities
{
    [Table(Name = "Roles")]
    public class RoleEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public short RoleId { get; set; }
        [Column]
        public string RoleName { get; set; }
        [Column]
        public string RoleDescription { get; set; }
    }
}
