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

using System.Configuration;

namespace sBlog.Net.Infrastructure
{
    public class SblogNetSettingsConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("enableMiniProfiler", DefaultValue = false)]
        public bool EnableMiniProfiler
        {
            get { return (bool) this["enableMiniProfiler"]; }
            set { this["enableMiniProfiler"] = value; }
        }
    }
}