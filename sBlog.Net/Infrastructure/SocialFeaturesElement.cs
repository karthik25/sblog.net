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
    public class SocialFeaturesElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = false)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("twitterId", DefaultValue = "")]
        public string TwitterId
        {
            get { return (string)this["twitterId"]; }
            set { this["twitterId"] = value; }
        }

        [ConfigurationProperty("githubId", DefaultValue = "")]
        public string GithubId
        {
            get { return (string)this["githubId"]; }
            set { this["githubId"] = value; }
        }

        [ConfigurationProperty("facebookId", DefaultValue = "")]
        public string FacebookId
        {
            get { return (string)this["facebookId"]; }
            set { this["facebookId"] = value; }
        }

        [ConfigurationProperty("pinterestId", DefaultValue = "")]
        public string PinterestId
        {
            get { return (string)this["pinterestId"]; }
            set { this["pinterestId"] = value; }
        }
    }
}