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
using sBlog.Net.Domain.Interfaces;
using System.IO;

namespace sBlog.Net.Areas.Setup.Services
{
    public static class UploadFolderVerifier
    {
        private const string TestFileName = "sblog-test.txt";

        public static bool CanSaveOrDeleteFiles(IPathMapper pathMapper)
        {
            return CreateFile(pathMapper);
        }

        private static bool CreateFile(IPathMapper pathMapper)
        {
            try
            {
                var sWriter = new StreamWriter(pathMapper.MapPath("~/Uploads/" + TestFileName));
                sWriter.WriteLine("Testing sBlog.Net install");
                sWriter.Close();

                return DeleteFile(pathMapper);
            }
            catch
            {
                return false;
            }
        }

        private static bool DeleteFile(IPathMapper pathMapper)
        {
            try
            {
                var filePath = pathMapper.MapPath("~/Uploads/" + TestFileName);
                File.Delete(filePath);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
