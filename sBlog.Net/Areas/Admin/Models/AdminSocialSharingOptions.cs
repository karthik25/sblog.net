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
using System.Collections.Generic;

namespace sBlog.Net.Areas.Admin.Models
{
    public class AdminSocialSharingOptions : AdminBaseViewModel
    {
        public string Name { get; set; }
        public string ImageClass { get; set; }
        public bool IsEnabled { get; set; }
        public List<AdminSocialSharingOption> Options { get; set; }

        public static AdminSocialSharingOptions Create(int selectedValue, bool updateStatus = false)
        {
            return new AdminSocialSharingOptions
            {
                Name = "selectedSharingOption",
                ImageClass = "imgSharing",
                Options = new List<AdminSocialSharingOption>
                {
                    new AdminSocialSharingOption { Value = 1, ImageUrl = "/Content/Images/Classic1.png", Selected = selectedValue == 1 },
                    new AdminSocialSharingOption { Value = 2, ImageUrl = "/Content/Images/Multichannel.png", Selected = selectedValue == 2 },
                    new AdminSocialSharingOption { Value = 3, ImageUrl = "/Content/Images/Horizontal.png", Selected = selectedValue == 3 },
                    new AdminSocialSharingOption { Value = 4, ImageUrl = "/Content/Images/Vertical.png", Selected = selectedValue == 4 }
                },
                UpdateStatus = updateStatus
            };
        }
    }

    public class AdminSocialSharingOption
    {
        public int Value { get; set; }
        public string ImageUrl { get; set; }
        public bool Selected { get; set; }
    }
}
