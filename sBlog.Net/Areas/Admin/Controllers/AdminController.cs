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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Models;
using sBlog.Net.Providers;
using sBlog.Net.Controllers;
using sBlog.Net.Infrastructure;
using sBlog.Net.Models.Account;
using sBlog.Net.Domain.Entities;
using sBlog.Net.ShortCodeManager;
using sBlog.Net.Domain.Utilities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Generators;
using sBlog.Net.Areas.Admin.Models;
using System.Text.RegularExpressions;

namespace sBlog.Net.Areas.Admin.Controllers
{
    public class AdminController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly IComment _commentRepository;
        private readonly ICategory _categoryRepository;
        private readonly ITag _tagRepository;
        private readonly IPathMapper _pathMapper;
        private readonly IUser _userRepository;

        public AdminController(IPost postRepository, IComment commentRepository, ICategory categoryRepository, ITag tagRepository, ISettings settingsRepository, IPathMapper pathMapper, IUser userRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _pathMapper = pathMapper;
            _userRepository = userRepository;
            ExpectedMasterName = string.Empty;

            IsAdminController = true;
        }

        [Authorize]
        public ActionResult Index()
        {
            var comments = _commentRepository.GetAllComments();
            var posts = _postRepository.GetPostsByUserID(GetUserId()).Select(p => p.PostID);
            var filteredComments = comments.Where(c => posts.Contains(c.PostID)).ToList();

            var model = new AdminDashboardViewModel
            {
                PostCount = _postRepository.GetPostsByUserID(GetUserId(), 1).Count,
                PagesCount = _postRepository.GetPostsByUserID(GetUserId(), 2).Count, // at this point non-admin users cannot add pages
                CategoriesCount = _categoryRepository.GetCategories().Count,
                TagsCount = _tagRepository.GetAllTags().Count,

                AllCommentsCount = filteredComments.Count(),
                ApprovedCount = filteredComments.Count(c => c.CommentStatus == 0),
                PendingCount = filteredComments.Count(c => c.CommentStatus == 1),
                SpamCount = filteredComments.Count(c => c.CommentStatus == 2),

                CanView = GetUserId() == 1,

                BlogName = SettingsRepository.BlogName
            };

            return View(model);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Settings()
        {
            var akismetUrl = string.IsNullOrEmpty(SettingsRepository.BlogAkismetUrl)
                                 ? GetRootUrl().TrimEnd('/')
                                 : SettingsRepository.BlogAkismetUrl;
            var adminSettings = new AdminSettingsViewModel
            {
                BlogName = SettingsRepository.BlogName,
                BlogCaption = SettingsRepository.BlogCaption,
                BlogTheme = SettingsRepository.BlogTheme,
                BlogThemes = GetAvailableThemes(SettingsRepository.BlogTheme),
                BlogSocialSharing = SettingsRepository.BlogSocialSharing,
                BlogSyntaxHighlighting = SettingsRepository.BlogSyntaxHighlighting,
                PostsPerPage = SettingsRepository.BlogPostsPerPage,
                AkismetEnabled = SettingsRepository.BlogAkismetEnabled,
                AkismetDeleteSpam = SettingsRepository.BlogAkismetDeleteSpam,
                AkismetKey = SettingsRepository.BlogAkismetKey,
                AkismetUrl = akismetUrl,
                AdminEmailAddress = SettingsRepository.BlogAdminEmailAddress,
                BlogSmtpAddress = SettingsRepository.BlogSmtpAddress,
                ManageItemsPerPage = SettingsRepository.ManageItemsPerPage,
                BlogErrorAction = SettingsRepository.BlogSiteErrorEmailAction,
                Title = SettingsRepository.BlogName,
                DisqusEnabled = SettingsRepository.DisqusEnabled,
                DisqusShortName = SettingsRepository.BlogDisqusShortName
            };
            return View(adminSettings);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult Settings(AdminSettingsViewModel adminSettingsViewModel)
        {
            adminSettingsViewModel.BlogThemes = GetAvailableThemes(adminSettingsViewModel.BlogTheme);

            if (ModelState.IsValid && ValidateAkismetSettings(adminSettingsViewModel))
            {
                SettingsRepository.BlogName = adminSettingsViewModel.BlogName;
                SettingsRepository.BlogCaption = adminSettingsViewModel.BlogCaption;
                SettingsRepository.BlogTheme = adminSettingsViewModel.BlogTheme;
                SettingsRepository.BlogSocialSharing = adminSettingsViewModel.BlogSocialSharing;
                SettingsRepository.BlogSyntaxHighlighting = adminSettingsViewModel.BlogSyntaxHighlighting;
                SettingsRepository.BlogPostsPerPage = adminSettingsViewModel.PostsPerPage;
                SettingsRepository.BlogAkismetEnabled = adminSettingsViewModel.AkismetEnabled;
                SettingsRepository.BlogAkismetDeleteSpam = adminSettingsViewModel.AkismetDeleteSpam;
                SettingsRepository.BlogAkismetKey = adminSettingsViewModel.AkismetKey;
                SettingsRepository.BlogAkismetUrl = adminSettingsViewModel.AkismetUrl;
                SettingsRepository.BlogAdminEmailAddress = adminSettingsViewModel.AdminEmailAddress;
                SettingsRepository.BlogSmtpAddress = adminSettingsViewModel.BlogSmtpAddress;

                if (!string.IsNullOrEmpty(adminSettingsViewModel.BlogSmtpPassword))
                {
                    SettingsRepository.BlogSmtpPassword = TripleDES.EncryptString(adminSettingsViewModel.BlogSmtpPassword);
                }

                SettingsRepository.ManageItemsPerPage = adminSettingsViewModel.ManageItemsPerPage;
                SettingsRepository.BlogSiteErrorEmailAction = adminSettingsViewModel.BlogErrorAction;
                SettingsRepository.DisqusEnabled = adminSettingsViewModel.DisqusEnabled;
                SettingsRepository.BlogDisqusShortName = adminSettingsViewModel.DisqusShortName;
            }

            adminSettingsViewModel.UpdateStatus = true;
            adminSettingsViewModel.Title = SettingsRepository.BlogName;

            return View(adminSettingsViewModel);
        }

        private bool ValidateAkismetSettings(AdminSettingsViewModel adminSettingsViewModel)
        {
            if (!adminSettingsViewModel.AkismetEnabled)
                return true;

            Uri url;
            if (string.IsNullOrEmpty(adminSettingsViewModel.AkismetUrl) || !Uri.TryCreate(adminSettingsViewModel.AkismetUrl, UriKind.Absolute, out url))
            {
                ModelState.AddModelError("Akismet_Url", "Akismet url entered is invalid");
                return false;
            }

            if (string.IsNullOrEmpty(adminSettingsViewModel.AkismetKey))
            {
                ModelState.AddModelError("Akismet_Key", "Akismet key entered is invalid");
                return false;
            }

            return true;
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EnableSocialSharing(bool enableSocialSharing)
        {
            if (enableSocialSharing)
                SettingsRepository.BlogSocialSharing = true;
            return RedirectToRoute("AdminSocialSharingOptions");
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SocialSharingOptions()
        {
            var selectedSharing = SettingsRepository.BlogSocialSharingChoice;
            var adminSocialSharingOptions = AdminSocialSharingOptions.Create(selectedSharing);
            adminSocialSharingOptions.Title = SettingsRepository.BlogName;
            adminSocialSharingOptions.IsEnabled = SettingsRepository.BlogSocialSharing;
            return View(adminSocialSharingOptions);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult SocialSharingOptions(int selectedSharingOption)
        {
            SettingsRepository.BlogSocialSharingChoice = selectedSharingOption;
            var adminSocialSharingOptions = AdminSocialSharingOptions.Create(selectedSharingOption, true);
            adminSocialSharingOptions.Title = SettingsRepository.BlogName;
            adminSocialSharingOptions.IsEnabled = SettingsRepository.BlogSocialSharing;
            return View(adminSocialSharingOptions);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EnableSyntaxHighlighter(bool enableSyntaxHighlighter)
        {
            if (enableSyntaxHighlighter)
                SettingsRepository.BlogSyntaxHighlighting = true;
            return RedirectToRoute("AdminSyntaxHighlighterOptions");
        }

        [Authorize]
        public ActionResult SyntaxHighlighterOptions()
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin", new { Area = "Admin" });
            }

            var currentTheme = SettingsRepository.BlogSyntaxTheme;
            var selectedItems = SettingsRepository.BlogSyntaxScripts;
            var model = new SyntaxHighlighterViewModel
            {
                Brushes = GetBrushesModel(selectedItems),
                AvailableThemes = GetAvailableSyntaxThemes(currentTheme),
                EditThemeAttributes = GetAttributes(GetUserId()),
                Title = SettingsRepository.BlogName,
                IsEnabled = SettingsRepository.BlogSyntaxHighlighting,
                CanEnable = GetUserId() == 1
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SyntaxHighlighterOptions(string Theme, CheckBoxListViewModel selectedBrushes)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var userId = GetUserId();
            var updatedTheme = userId == 1 ? Theme : SettingsRepository.BlogSyntaxTheme;
            SettingsRepository.BlogSyntaxScripts = string.Join("~", selectedBrushes.GetSelectedItems());
            SettingsRepository.BlogSyntaxTheme = updatedTheme;

            var model = new SyntaxHighlighterViewModel
            {
                Brushes = selectedBrushes,
                AvailableThemes = GetAvailableSyntaxThemes(updatedTheme),
                EditThemeAttributes = GetAttributes(userId),
                Title = SettingsRepository.BlogName,
                IsEnabled = SettingsRepository.BlogSyntaxHighlighting,
                UpdateStatus = true,
                CanEnable = GetUserId() == 1
            };

            return View(model);
        }

        private static IDictionary<string, object> GetAttributes(int userId)
        {
            IDictionary<string, object> editAttributes = new Dictionary<string, object> { { "class", "dropDownBox" } };
            if (userId != 1)
            {
                editAttributes.Add("disabled", "disabled");
            }
            return editAttributes;
        }

        [Authorize]
        public ActionResult UpdateProfile()
        {
            var userEntity = _userRepository.GetUserObjByUserID(GetUserId());
            var model = new UpdateProfileModel
            {
                UserDisplayName = userEntity.UserDisplayName,
                UserEmailAddress = userEntity.UserEmailAddress,
                Title = SettingsRepository.BlogName,
                UserSite = userEntity.UserSite
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProfile(UpdateProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var userEntity = GetUserEntity(model);
                var status = _userRepository.UpdateProfile(userEntity);
                if (!status)
                {
                    ModelState.AddModelError("__FORM", "Unable to update the profile. Please try again later or contact the administrator");
                }
                else
                {
                    model.UpdateStatus = true;
                }
            }

            model.Title = SettingsRepository.BlogName;
            return View(model);
        }

        public ActionResult AdminShortcuts()
        {
            return PartialView("AdminShortcuts");
        }

        private UserEntity GetUserEntity(UpdateProfileModel model)
        {
            var userEntity = new UserEntity
            {
                UserID = GetUserId(),
                UserDisplayName = model.UserDisplayName,
                UserEmailAddress = model.UserEmailAddress,
                UserSite = model.UserSite
            };

            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                var randomCode = RandomStringGenerator.RandomString();
                userEntity.Password = PasswordHelper.GenerateHashedPassword(model.NewPassword, randomCode);
                userEntity.UserCode = TripleDES.EncryptString(randomCode);
            }

            return userEntity;
        }

        public ActionResult SyntaxHighlighterHelp()
        {
            var selectedBrushes = SettingsRepository.BlogSyntaxScripts;
            var syntaxPossibilitiesViewModel = new SyntaxPossibilitiesViewModel
            {
                SyntaxPossibilities = new SyntaxPossibilities(_pathMapper, selectedBrushes),
                IsEnabled = SettingsRepository.BlogSyntaxHighlighting
            };
            return PartialView(syntaxPossibilitiesViewModel);
        }

        public ActionResult Error()
        {
            var errType = Request.QueryString["err"];

            var blogErrorViewModel = new BlogErrorViewModel
            {
                Title = SettingsRepository.BlogName,
                ErrorCode = errType,
                ErrorDescription = "An unknown error has occurred"
            };

            return View(blogErrorViewModel);
        }

        private List<SelectListItem> GetAvailableSyntaxThemes(string selectedItem)
        {
            var items = new List<SelectListItem>();
            var basePath = _pathMapper.MapPath("~/Content/codeHighlighter/styles");
            var files = Directory.GetFiles(basePath, "shCore*.css");

            files.ToList().ForEach(file =>
            {
                var r1 = new Regex(@"shCore([A-Za-z0-9\-]+).css");
                var match = r1.Match(Path.GetFileName(file));
                if (match.Groups[1].Value != string.Empty)
                {
                    var item = new SelectListItem { Text = match.Groups[1].Value, Value = match.Groups[1].Value, Selected = match.Groups[1].Value == selectedItem };
                    items.Add(item);
                }
            });

            var defaultItem = items.Single(i => i.Text == "Default");
            defaultItem.Selected = true;

            return items;
        }

        private CheckBoxListViewModel GetBrushesModel(string selectedItems)
        {
            var basePath = _pathMapper.MapPath("~/Content/codeHighlighter/scripts");
            return SyntaxHighlighterBrushesModel.GetBrushesModel(basePath, selectedItems);
        }

        private List<SelectListItem> GetAvailableThemes(string selectedTheme)
        {
            var baseDirectory = _pathMapper.MapPath("~/Themes");
            var directories = Directory.GetDirectories(baseDirectory);
            return directories.Select(directory => directory.Split('\\')).Select(split => new SelectListItem { Text = Regex.Replace(split.Last(), "(\\B[A-Z])", " $1"), Value = split.Last(), Selected = split.Last() == selectedTheme }).ToList();
        }
    }
}
