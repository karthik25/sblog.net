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

namespace sBlog.Net.Configuration
{
    public class SblogNetSettingsConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("connectionString", DefaultValue = "")]
        public string ConnectionString
        {
            get { return (string) this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        [ConfigurationProperty("enableMiniProfiler", DefaultValue = false)]
        public bool EnableMiniProfiler
        {
            get { return (bool) this["enableMiniProfiler"]; }
            set { this["enableMiniProfiler"] = value; }
        }

        [ConfigurationProperty("hasherFullyQualifiedTypeName")]
        public string HasherFullyQualifiedTypeName
        {
            get { return (string)this["hasherFullyQualifiedTypeName"]; }
            set { this["hasherFullyQualifiedTypeName"] = value; }
        }

        [ConfigurationProperty("cacheDuration")]
        public string CacheDuration
        {
            get { return (string)this["cacheDuration"]; }
            set { this["cacheDuration"] = value; }
        }

        [ConfigurationProperty("bitlyUserName")]
        public string BitlyUserName
        {
            get { return (string)this["bitlyUserName"]; }
            set { this["bitlyUserName"] = value; }
        }

        [ConfigurationProperty("bitlyApiKey")]
        public string BitlyApiKey
        {
            get { return (string)this["bitlyApiKey"]; }
            set { this["bitlyApiKey"] = value; }
        }

        [ConfigurationProperty("editorType")]
        public string EditorType
        {
            get { return (string) this["editorType"]; }
            set { this["editorType"] = value; }
        }

        [ConfigurationProperty("socialFeatures")]
        public SocialFeaturesElement SocialFeatures
        {
            get { return (SocialFeaturesElement) this["socialFeatures"]; }
            set { this["socialFeatures"] = value; }
        }

        [ConfigurationProperty("theme")]
        public ThemeElement Theme
        {
            get { return (ThemeElement)this["theme"]; }
            set { this["theme"] = value; }
        }
    }
}
