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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Domain.Interfaces;
using System.Text.RegularExpressions;
using System.ComponentModel;
using sBlog.Net.Models;
using sBlog.Net.Areas.Admin.Models.JsonEntities;
using sBlog.Net.Controllers;

namespace sBlog.Net.Areas.Admin.Controllers
{
    public class UploadsController : BlogController
    {
        private readonly IPathMapper _pathMapper;

        private readonly int _itemsPerPage;

        public UploadsController(IPathMapper pathMapper, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _pathMapper = pathMapper;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ManageUploads([DefaultValue(1)] int page)
        {
            var basePath = _pathMapper.MapPath(UploadsBasePath);
            var fileInfoList = new List<FileEntry>();

            var files = Directory.GetFiles(basePath).ToList();
            files.ForEach(file => fileInfoList.Add(GetFileEntry(file)));

            var fileInfoListSorted = fileInfoList.OrderBy(f => f.FileName).ToList();

            var model = new AdminUploadsViewModel
            {
                FileEntries = fileInfoListSorted.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = fileInfoList.Count
                },
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };

            return View(model);
        }

        [Authorize]
        public ActionResult SelectFile(string CKEditorFuncNum)
        {
            var basePath = _pathMapper.MapPath(UploadsBasePath);
            var fileInfoList = new List<FileEntry>();

            var files = Directory.GetFiles(basePath);
            files.ToList().ForEach(file => fileInfoList.Add(new FileEntry { FileName = Path.GetFileName(file), FileIconName = GetFileInfoByExt(Path.GetExtension(file)) }));

            var model = new AdminUploadsViewModel
            {
                FileEntries = fileInfoList.OrderBy(f => f.FileName).ToList(),
                CKEditorFuncNum = CKEditorFuncNum
            };

            return View(model);
        }

        [Authorize]
        public ActionResult UploadFile(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var uploadStatus = "Upload Complete";
            try
            {
                var fileName = GetSafeFileName(upload.FileName);
                var basePath = _pathMapper.MapPath(UploadsBasePath);
                upload.SaveAs(basePath + "\\" + fileName);
            }
            catch
            {
                uploadStatus = "An error occured. Please try again later.";
            }
            return View("UploadFile", (object)uploadStatus);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult UploadFileWithRefresh(HttpPostedFileBase file)
        {
            try
            {
                var fileName = GetSafeFileName(file.FileName);
                var basePath = _pathMapper.MapPath(UploadsBasePath);
                file.SaveAs(basePath + "\\" + fileName);
            }
            catch
            {
            }
            return RedirectToRoute("AdminUploads");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public JsonResult DeleteUploadedFile(string fileName, string token)
        {
            if (!CheckToken(token))
            {
                throw new Exception("Possible unauthorized access");
            }

            var status = "File Deleted";
            try
            {
                var basePath = _pathMapper.MapPath(UploadsBasePath);
                if (System.IO.File.Exists(basePath + "\\" + fileName))
                {
                    System.IO.File.Delete(basePath + "\\" + fileName);
                }
                else
                {
                    status = "File unavailable";
                }
            }
            catch
            {
                status = "Unable to delete the file";
            }
            return Json(new FileEntry { FileName = fileName, FileStatus = status }, JsonRequestBehavior.AllowGet);
        }

        private FileEntry GetFileEntry(string file)
        {
            var fileName = Path.GetFileName(file);
            var urlHelper = new UrlHelper(HttpContext.Request.RequestContext);
            return new FileEntry
            {
                FileName = fileName,
                FileIconName = GetFileInfoByExt(Path.GetExtension(file)),
                FileUrl = urlHelper.Content(UploadsBasePath + "/" + fileName)
            };
        }

        private static string GetSafeFileName(string currentName)
        {
            var safeName = Regex.Replace(currentName, @"[^A-Za-z0-9_.]",
                                "-", RegexOptions.IgnoreCase);
            return safeName;
        }

        private string GetFileInfoByExt(string ext)
        {
            var basePath = _pathMapper.MapPath("~/Content/Images/extension_icons");
            var files = Directory.GetFiles(basePath).ToList();

            var fileImg = files.SingleOrDefault(f => ext.TrimStart('.').ToLower() == Path.GetFileNameWithoutExtension(f).ToLower());
            var requiredImg = fileImg ?? "unknown.png";

            return Path.GetFileName(requiredImg);
        }

        private const string UploadsBasePath = "~/Uploads";
    }
}
