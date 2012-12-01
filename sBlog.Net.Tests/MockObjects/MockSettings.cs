using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockSettings : ISettings
    {
        private List<SettingsEntity> BlogSettings { get; set; }

        public MockSettings(int loadType = 1)
        {
            switch (loadType)
            {
                case 1:
                    BlogSettings = LoadSettings();
                    break;
                case 2:
                    BlogSettings = LoadSettingsWithAkismetEnabled();
                    break;
                default:
                    BlogSettings = LoadSettingsWithAkismetEnabledAndDeleteEnabled();
                    break;
            }
        }

        public string BlogName
        {
            get
            {
                var blogName = GetValue("BlogName");
                return blogName ?? "sBlog.Net";
            }
            set
            {
                var blogName = GetValueInternal(value) ?? "sBlog.Net";
                UpdateSettings("BlogName", blogName);
            }
        }

        public string BlogCaption
        {
            get
            {
                var blogCaption = GetValue("BlogCaption");
                return blogCaption ?? "Just another .net site";
            }
            set
            {
                var blogCaption = GetValueInternal(value) ?? "Just another .net site";
                UpdateSettings("BlogCaption", blogCaption);
            }
        }

        public int BlogPostsPerPage
        {
            get
            {
                int postsPerPage;
                if (!Int32.TryParse(GetValue("BlogPostsPerPage"), out postsPerPage))
                    postsPerPage = 5;
                return postsPerPage;
            }
            set
            {
                UpdateSettings("BlogPostsPerPage", value.ToString());
            }
        }

        public string BlogTheme
        {
            get
            {
                var blogTheme = GetValue("BlogTheme");
                return blogTheme ?? "PerfectBlemish";
            }
            set
            {
                var blogTheme = GetValueInternal(value) ?? "PerfectBlemish";
                UpdateSettings("BlogTheme", blogTheme);
            }
        }

        public bool BlogSocialSharing
        {
            get
            {
                var blogSharing = GetValue("BlogSocialSharing");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSocialSharing", value.ToString());
            }
        }

        public bool BlogSyntaxHighlighting
        {
            get
            {
                var blogSharing = GetValue("BlogSyntaxHighlighting");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSyntaxHighlighting", value.ToString());
            }
        }

        public string BlogSyntaxTheme
        {
            get
            {
                var blogTheme = GetValue("BlogSyntaxTheme");
                return blogTheme ?? "Default";
            }
            set
            {
                var blogTheme = GetValueInternal(value) ?? "Default";
                UpdateSettings("BlogSyntaxTheme", blogTheme);
            }
        }

        public string BlogSyntaxScripts
        {
            get
            {
                var blogSyntaxScripts = GetValue("BlogStnyaxScripts");
                return blogSyntaxScripts ?? "CSharp";
            }
            set
            {
                var blogSyntaxScripts = GetValueInternal(value) ?? "CSharp";
                UpdateSettings("BlogStnyaxScripts", blogSyntaxScripts);
            }
        }

        public bool BlogAkismetEnabled
        {
            get
            {
                var blogAkismetEnabled = GetValue("BlogAkismetEnabled");
                bool result;
                if (!bool.TryParse(blogAkismetEnabled, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogAkismetEnabled", value.ToString());
            }
        }

        public string BlogAkismetKey
        {
            get
            {
                var blogAkismetKey = GetValue("BlogAkismetKey");
                return blogAkismetKey ?? string.Empty;
            }
            set
            {
                var blogAkismetKey = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAkismetKey", blogAkismetKey);
            }
        }

        public string BlogAkismetUrl
        {
            get
            {
                var blogAkismetUrl = GetValue("BlogAkismetUrl");
                return blogAkismetUrl ?? string.Empty;
            }
            set
            {
                var blogAkismetUrl = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAkismetUrl", blogAkismetUrl);
            }
        }

        public bool BlogAkismetDeleteSpam
        {
            get
            {
                var blogAkismetDeleteSpam = GetValue("BlogAkismetDeleteSpam");
                bool result;
                if (!bool.TryParse(blogAkismetDeleteSpam, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogAkismetDeleteSpam", value.ToString());
            }
        }

        public int BlogSocialSharingChoice
        {
            get
            {
                int blogSocialSharingChoice;
                if (!Int32.TryParse(GetValue("BlogSocialSharingChoice"), out blogSocialSharingChoice))
                    blogSocialSharingChoice = 2;
                return blogSocialSharingChoice;
            }
            set
            {
                UpdateSettings("BlogSocialSharingChoice", value.ToString());
            }
        }

        public bool BlogSiteErrorEmailAction
        {
            get
            {
                var blogSiteErrorAction = GetValue("BlogSiteErrorEmailAction");
                bool result;
                if (!bool.TryParse(blogSiteErrorAction, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSiteErrorEmailAction", value.ToString());
            }
        }

        public string BlogAdminEmailAddress
        {
            get
            {
                var blogEmail = GetValue("BlogAdminEmailAddress");
                return blogEmail ?? string.Empty;
            }
            set
            {
                string blogEmail = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAdminEmailAddress", blogEmail);
            }
        }

        public string BlogSmtpAddress
        {
            get
            {
                var smtpAddress = GetValue("BlogSmtpAddress");
                return smtpAddress ?? string.Empty;
            }
            set
            {
                var smtpAddress = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogSmtpAddress", smtpAddress);
            }
        }

        public string BlogSmtpPassword
        {
            get
            {
                var smtpAddress = GetValue("BlogSmtpPassword");
                return smtpAddress ?? string.Empty;
            }
            set
            {
                var smtpAddress = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogSmtpPassword", smtpAddress);
            }
        }

        public bool InstallationComplete
        {
            get
            {
                var installationComplete = GetValue("InstallationComplete");
                bool result;
                if (!bool.TryParse(installationComplete, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("InstallationComplete", value.ToString());
            }
        }

        public int ManageItemsPerPage
        {
            get
            {
                int manageItemsPerPage;
                if (!Int32.TryParse(GetValue("ManageItemsPerPage"), out manageItemsPerPage))
                    manageItemsPerPage = 5;
                return manageItemsPerPage;
            }
            set
            {
                UpdateSettings("ManageItemsPerPage", value.ToString());
            }
        }

        public string GetValue(string key)
        {
            string value = null;
            var setting = BlogSettings.SingleOrDefault(s => s.KeyName == key);
            if (setting != null)
                value = setting.KeyValue;
            return value;
        }

        public bool UpdateSettings(string key, string value)
        {
            throw new NotImplementedException();
        }

        private string GetValueInternal(string value)
        {
            string _value = value;
            if (value == null || value.Trim() == string.Empty)
                _value = null;
            return _value;
        }

        private List<SettingsEntity> LoadSettings()
        {
            var settings = new List<SettingsEntity>
                               {
                                   new SettingsEntity {KeyName = "BlogName", KeyValue = "the .net way"},
                                   new SettingsEntity {KeyName = "BlogCaption", KeyValue = "Just another .net blog!!!"},
                                   new SettingsEntity {KeyName = "BlogPostsPerPage", KeyValue = "5"},
                                   new SettingsEntity {KeyName = "BlogTheme", KeyValue = "PerfectBlemish"},
                                   new SettingsEntity {KeyName = "BlogSocialSharing", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxHighlighting", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxTheme", KeyValue = "Django"},
                                   new SettingsEntity
                                       {KeyName = "BlogStnyaxScripts", KeyValue = "AppleScript~AS3~CSharp~Ruby"},
                                   new SettingsEntity {KeyName = "BlogAkismetEnabled", KeyValue = "False"},
                                   new SettingsEntity {KeyName = "BlogAkismetKey", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetUrl", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetDeleteSpam", KeyValue = "False"}
                               };

            return settings;
        }

        private List<SettingsEntity> LoadSettingsWithAkismetEnabled()
        {
            var settings = new List<SettingsEntity>
                               {
                                   new SettingsEntity {KeyName = "BlogName", KeyValue = "the .net way"},
                                   new SettingsEntity {KeyName = "BlogCaption", KeyValue = "Just another .net blog!!!"},
                                   new SettingsEntity {KeyName = "BlogPostsPerPage", KeyValue = "5"},
                                   new SettingsEntity {KeyName = "BlogTheme", KeyValue = "PerfectBlemish"},
                                   new SettingsEntity {KeyName = "BlogSocialSharing", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxHighlighting", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxTheme", KeyValue = "Django"},
                                   new SettingsEntity
                                       {KeyName = "BlogStnyaxScripts", KeyValue = "AppleScript~AS3~CSharp~Ruby"},
                                   new SettingsEntity {KeyName = "BlogAkismetEnabled", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogAkismetKey", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetUrl", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetDeleteSpam", KeyValue = "False"}
                               };

            return settings;
        }

        private List<SettingsEntity> LoadSettingsWithAkismetEnabledAndDeleteEnabled()
        {
            var settings = new List<SettingsEntity>
                               {
                                   new SettingsEntity {KeyName = "BlogName", KeyValue = "the .net way"},
                                   new SettingsEntity {KeyName = "BlogCaption", KeyValue = "Just another .net blog!!!"},
                                   new SettingsEntity {KeyName = "BlogPostsPerPage", KeyValue = "5"},
                                   new SettingsEntity {KeyName = "BlogTheme", KeyValue = "PerfectBlemish"},
                                   new SettingsEntity {KeyName = "BlogSocialSharing", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxHighlighting", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogSyntaxTheme", KeyValue = "Django"},
                                   new SettingsEntity
                                       {KeyName = "BlogStnyaxScripts", KeyValue = "AppleScript~AS3~CSharp~Ruby"},
                                   new SettingsEntity {KeyName = "BlogAkismetEnabled", KeyValue = "True"},
                                   new SettingsEntity {KeyName = "BlogAkismetKey", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetUrl", KeyValue = ""},
                                   new SettingsEntity {KeyName = "BlogAkismetDeleteSpam", KeyValue = "True"}
                               };

            return settings;
        }
    }
}
