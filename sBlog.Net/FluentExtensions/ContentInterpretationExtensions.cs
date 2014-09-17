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
using sBlog.Net.DependencyManagement;

namespace sBlog.Net.FluentExtensions
{
    public static class ContentInterpretationExtensions
    {
        public static bool IsMarkDown()
        {
            var settingsRepository = InstanceFactory.CreateSettingsInstance();
            return settingsRepository.EditorType == "markdown";
        }
    }
}