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
using System.Linq;

namespace sBlog.Net.Models
{
    public class CheckBoxListViewModel
    {
        public string HeaderText { get; set; }
        public List<CheckBoxListItem> Items { get; set; }

        public string[] GetSelectedItems()
        {
            return Items.Where(s => s.IsChecked).Select(s => s.Value).ToArray();
        }
    }

    public class CheckBoxListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
}
