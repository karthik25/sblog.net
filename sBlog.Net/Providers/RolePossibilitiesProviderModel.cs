using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.DependencyManagement;

namespace sBlog.Net.Providers
{
    public static class RolePossibilitiesProviderModel
    {
        public static List<SelectListItem> AvailableRoles
        {
            get
            {
                var roleRepository = InstanceFactory.CreateRoleInstance();
                return roleRepository.GetAllRoles()
                                     .Select(
                                         r => new SelectListItem
                                              {
                                                 Text = r.RoleDescription,
                                                 Value = r.RoleId.ToString(CultureInfo.InvariantCulture)
                                              })
                                     .ToList();
            }
        }
    }
}