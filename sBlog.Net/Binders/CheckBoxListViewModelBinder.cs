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
using System.Web.Mvc;
using System.Text.RegularExpressions;
using sBlog.Net.Models;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Binders
{
    public class CheckBoxListViewModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var models = new List<CheckBoxListViewModel>();            
            var formKeys = controllerContext.HttpContext.Request.Form.AllKeys.ToArray();

            var rootItems = formKeys.Where(s => s.StartsWith("hdrTitle")).ToList();

            if (rootItems.Count == 0)
                return null;

            foreach (var item in rootItems)
            {
                var hdrValue = item.Split('_')[1];
                var txtValues = formKeys.Where(s => s.StartsWith("lblLabel_" + hdrValue)).ToArray();
                var valValues = formKeys.Where(s => s.StartsWith("valValue_" + hdrValue)).ToArray();
                var hdnValues = formKeys.Where(s => s.StartsWith("hdnChk_" + hdrValue)).ToArray();

                var model = new CheckBoxListViewModel
                                {
                                    HeaderText = Regex.Replace(hdrValue, "([a-z])([A-Z])", "$1 $2"),
                                    Items = new List<CheckBoxListItem>()
                                };
                for (var index = 0; index < txtValues.Count(); index++)
                {
                    var listItem = new CheckBoxListItem
                                    {
                                        Text = bindingContext.GetValue(txtValues[index]),
                                        Value = bindingContext.GetValue(valValues[index]),
                                        IsChecked = bool.Parse(bindingContext.GetValue(hdnValues[index]))
                                    };

                    model.Items.Add(listItem);
                }

                models.Add(model);
            }

            return rootItems.Count == 1 ? (object) models.First() : models;
        }
    }
}
