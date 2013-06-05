using System.Collections.Generic;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Interfaces
{
    public interface IRole
    {
        List<RoleEntity> GetAllRoles();
        bool IsInRole(int userId, string roleName);
        void AddRoleForUser(int userId, short roleId);
        void RemoveRoleForUser(int userRoleId);
        void DeleteRolesForUser(int userId);
        short GetRoleForUser(int userId);
    }
}