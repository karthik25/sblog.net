using System;
using System.Linq;
using System.Web.Security;
using sBlog.Net.DependencyManagement;

namespace sBlog.Net.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var userInstance = InstanceFactory.CreateUserInstance();
            var user = userInstance.GetUserObjByUserName(username);
            if (user == null)
                return false;
            var roleInstance = InstanceFactory.CreateRoleInstance();
            return roleInstance.IsInRole(user.UserID, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var userInstance = InstanceFactory.CreateUserInstance();
            var user = userInstance.GetUserObjByUserName(username);
            if (user == null)
                return null;
            var roleInstance = InstanceFactory.CreateRoleInstance();
            var userRole = roleInstance.GetRoleForUser(user.UserID);
            return userRole == -1 ? new string[] { "Author" } : new[] { roleInstance.GetAllRoles().Single(r => r.RoleId == userRole).RoleName };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var roleInstance = InstanceFactory.CreateRoleInstance();
            return roleInstance.GetAllRoles().Any(r => r.RoleName == roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var roleInstance = InstanceFactory.CreateRoleInstance();
            return roleInstance.GetAllRoles().Select(r => r.RoleName).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}