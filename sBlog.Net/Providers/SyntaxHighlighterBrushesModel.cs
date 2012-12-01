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
using sBlog.Net.Models;
using System.Text.RegularExpressions;
using System.IO;

namespace sBlog.Net.Providers
{
    public static class SyntaxHighlighterBrushesModel
    {
        public static CheckBoxListViewModel GetBrushesModel(string basePath, string selectedItems)
        {
            var currentList = selectedItems.Split('~').ToList();
            var checkBoxListViewModel = new CheckBoxListViewModel {HeaderText = "Select Brushes"};

            var items = new List<CheckBoxListItem>();
            var files = Directory.GetFiles(basePath, "shBrush*.js");
            files.ToList().ForEach(file =>
            {
                var r1 = new Regex(@"shBrush([A-Za-z0-9\-]+).js");
                var match = r1.Match(Path.GetFileName(file));
                var item = new CheckBoxListItem { Text = match.Groups[1].Value, Value = match.Groups[1].Value, IsChecked = currentList.Contains(match.Groups[1].Value) };
                items.Add(item);
            });

            checkBoxListViewModel.Items = items;

            return checkBoxListViewModel;
        }
    }
}