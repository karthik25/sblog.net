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