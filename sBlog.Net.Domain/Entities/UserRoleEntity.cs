using System.Data.Linq.Mapping;

namespace sBlog.Net.Domain.Entities
{
    [Table(Name = "UserRoles")]
    public class UserRoleEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int UserRoleId { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public short RoleId { get; set; }
    }
}
