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
