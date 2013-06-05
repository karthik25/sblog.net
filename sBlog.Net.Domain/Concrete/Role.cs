using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Domain.Concrete
{
    public class Role : DefaultDisposable, IRole
    {
        private readonly Table<RoleEntity> _rolesTable;
        private readonly Table<UserRoleEntity> _userRoleMapping;

        public Role()
        {
            _rolesTable = context.GetTable<RoleEntity>();
            _userRoleMapping = context.GetTable<UserRoleEntity>();
        }

        public List<RoleEntity> GetAllRoles()
        {
            return _rolesTable.ToList();
        }

        public bool IsInRole(int userId, string roleName)
        {
            if (!_userRoleMapping.Any(u => u.UserId == userId))
                return false;
            var roleEntity = _rolesTable.SingleOrDefault(r => r.RoleName == roleName);
            return roleEntity != null && _userRoleMapping.Any(u => u.UserId == userId && u.RoleId == roleEntity.RoleId);
        }

        public void AddRoleForUser(int userId, short roleId)
        {
            var oldRole = _userRoleMapping.SingleOrDefault(u => u.UserId == userId);
            if (oldRole != null)
            {
                _userRoleMapping.DeleteOnSubmit(oldRole);
                context.SubmitChanges();
            }

            var userRoleEntity = new UserRoleEntity { UserId = userId, RoleId = roleId };
            _userRoleMapping.InsertOnSubmit(userRoleEntity);
            context.SubmitChanges();
        }

        public void RemoveRoleForUser(int userRoleId)
        {
            var userRoleEntity = _userRoleMapping.SingleOrDefault(r => r.UserRoleId == userRoleId);
            if (userRoleEntity != null)
            {
                _userRoleMapping.DeleteOnSubmit(userRoleEntity);
                context.SubmitChanges();
            }
        }

        public void DeleteRolesForUser(int userId)
        {
            var userRoles = _userRoleMapping.Where(u => u.UserId == userId);
            if (userRoles.Any())
            {
                _userRoleMapping.DeleteAllOnSubmit(userRoles);
                context.SubmitChanges();
            }
        }

        public short GetRoleForUser(int userId)
        {
            var userRoleEntity = _userRoleMapping.SingleOrDefault(r => r.UserId == userId);
            return (short)(userRoleEntity == null ? -1 : userRoleEntity.RoleId);
        }
    }
}
