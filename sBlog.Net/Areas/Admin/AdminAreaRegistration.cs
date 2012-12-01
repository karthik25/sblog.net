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
using System.Web.Mvc;

namespace sBlog.Net.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("AdminSettings", "admin/settings",
                             new { controller = "Admin", action = "Settings" });

            context.MapRoute("AdminPosts", "admin/posts",
                            new { controller = "Post", action = "ManagePosts" });

            context.MapRoute("AdminPostsAdd", "admin/post/add",
                            new { controller = "Post", action = "Add" });

            // unused
            context.MapRoute("AdminPostsEdit", "admin/post/edit/{postId}",
                            new { controller = "Post", action = "Add" },
                            new { postId = @"\d+" });

            context.MapRoute("AdminPages", "admin/pages",
                            new { controller = "Page", action = "ManagePages" });

            context.MapRoute("AdminPagesAdd", "admin/page/add",
                            new { controller = "Page", action = "Add" });

            // unused
            context.MapRoute("AdminPagesEdit", "admin/page/edit/{postId}",
                            new { controller = "Page", action = "Edit" },
                            new { postId = @"\d+" });

            context.MapRoute("AdminCategories", "admin/categories",
                             new { controller = "CategoryAdmin", action = "ManageCategories" });

            context.MapRoute("AdminTags", "admin/tags",
                             new { controller = "TagAdmin", action = "ManageTags" });

            context.MapRoute("AdminComments", "admin/comments",
                             new { controller = "CommentAdmin", action = "ManageComments" });

            context.MapRoute("AdminUploads", "admin/uploads",
                             new { controller = "Uploads", action = "ManageUploads" });

            context.MapRoute("AdminSyntaxHighlighterOptions", "admin/syntax-highlighter-options",
                             new { controller = "Admin", action = "SyntaxHighlighterOptions" });

            context.MapRoute("AdminSocialSharingOptions", "admin/social-sharing-options",
                             new { controller = "Admin", action = "SocialSharingOptions" });

            context.MapRoute("AdminUserManagement", "admin/user-management",
                             new { controller = "UserAdmin", action = "UserManagement" });

            context.MapRoute("AdminPostManagement", "admin/user-management/manage-public-posts",
                             new { controller = "UserAdmin", action = "ManagePublicPosts" });

            context.MapRoute("AdminCommentManagement", "admin/user-management/manage-comments",
                             new { controller = "UserAdmin", action = "ManagePublicComments" });

            context.MapRoute("AdminUpdateProfile", "admin/update-profile",
                             new { controller = "Admin", action = "UpdateProfile" });

            context.MapRoute("AdminError", "admin/error",
                             new { controller = "Admin", action = "Error" });

            context.MapRoute("AdminIndex", "admin",
                 new { controller = "Admin", action = "Index" });

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}",
                new { controller = "Admin", action = "Index" }
            );
        }
    }
}
