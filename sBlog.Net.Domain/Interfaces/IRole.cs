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
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Interfaces
{
    public interface IRole : IDisposable
    {
        List<RoleEntity> GetAllRoles();
        bool IsInRole(int userId, string roleName);
        void AddRoleForUser(int userId, short roleId);
        void RemoveRoleForUser(int userRoleId);
        void DeleteRolesForUser(int userId);
        short GetRoleForUser(int userId);
    }
}