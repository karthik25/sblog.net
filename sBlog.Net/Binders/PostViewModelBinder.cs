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
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Models;
using sBlog.Net.Domain.Entities;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Binders
{
    public class PostViewModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var postModel = new PostViewModel
                                {
                                    Post =
                                        new PostEntity
                                            {
                                                PostID = int.Parse(bindingContext.GetValue("Post.PostID")),
                                                PostTitle = bindingContext.GetValue("Post.PostTitle"),
                                                PostContent = bindingContext.GetValue("Post.PostContent"),
                                                PostUrl = bindingContext.GetValue("Post.PostUrl"),
                                                PostAddedDate = DateTime.Parse(bindingContext.GetValue("Post.PostAddedDate")),
                                                UserCanAddComments = bool.Parse(bindingContext.GetValue("Post.UserCanAddComments")),
                                                CanBeShared = bool.Parse(bindingContext.GetValue("Post.CanBeShared")),
                                                IsPrivate = bool.Parse(bindingContext.GetValue("Post.IsPrivate")),
                                                EntryType = byte.Parse(bindingContext.GetValue("Post.EntryType")),
                                                BitlyUrl = bindingContext.GetValue("Post.BitlyUrl"),
                                                BitlySourceUrl = bindingContext.GetValue("Post.BitlySourceUrl")
                                            }
                                };

            postModel.Post.Order = postModel.Post.EntryType == 2 ? (int?)GetOrder(bindingContext.GetValue("Post.Order")) : null;

            IModelBinder ckBinder = new CheckBoxListViewModelBinder();
            postModel.Categories = (CheckBoxListViewModel)ckBinder.BindModel(controllerContext, bindingContext);

            if (postModel.Post.EntryType == 1)
            {
                if (!postModel.Categories.Items.Any(c => c.IsChecked))
                {
                    var general = postModel.Categories.Items.SingleOrDefault(c => c.Value == "1");
                    if (general != null)
                    {
                        general.IsChecked = true;
                    }
                }

                postModel.Tags = bindingContext.GetValue("hdnAddedTags");
            }

            return postModel;
        }

        private static int GetOrder(object value)
        {
            int parsedValue;
            if (value == null || value.ToString() == string.Empty)
                return int.MaxValue;
            return int.TryParse(value.ToString(), out parsedValue) ? parsedValue : int.MaxValue;
        }
    }
}
