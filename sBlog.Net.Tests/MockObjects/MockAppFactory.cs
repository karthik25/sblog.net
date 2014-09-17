using Moq;
using System.Security.Principal;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockObjects
{
    public static class MockAppFactory
    {
        public static ISettings GetMockSettings(int loadType = 1)
        {
            var mockSettings = GetBasicSettingsMock();

            switch (loadType)
            {
                case 1:
                    break;
                case 2:
                    mockSettings.SetupProperty(x => x.BlogAkismetEnabled, true);
                    break;
                default:
                    mockSettings.SetupProperty(x => x.BlogAkismetEnabled, true);
                    mockSettings.SetupProperty(x => x.BlogAkismetDeleteSpam, true);
                    break;
            }

            return mockSettings.Object;
        }

        private static Mock<ISettings> GetBasicSettingsMock()
        {
            var settings = new Mock<ISettings>();

            settings.SetupProperty(x => x.BlogName, "the .net way");
            settings.SetupProperty(x => x.BlogCaption, "Just another .net blog!!!");
            settings.SetupProperty(x => x.BlogPostsPerPage, 5);
            settings.SetupProperty(x => x.BlogTheme, "PerfectBlemish");
            settings.SetupProperty(x => x.BlogSocialSharing, true);
            settings.SetupProperty(x => x.BlogSyntaxHighlighting, true);
            settings.SetupProperty(x => x.BlogSyntaxTheme, "Django");
            settings.SetupProperty(x => x.BlogSyntaxScripts, "AppleScript~AS3~CSharp~Ruby");
            settings.SetupProperty(x => x.BlogAkismetKey, string.Empty);
            settings.SetupProperty(x => x.BlogAkismetUrl, string.Empty);
            settings.SetupProperty(x => x.BlogAkismetEnabled, false);
            settings.SetupProperty(x => x.BlogAkismetDeleteSpam, false);
            settings.SetupProperty(x => x.EditorType, "html");

            return settings;
        }

        public static IIdentity GetMockUserIdentity(int userId)
        {
            var iIdentity = new Mock<IIdentity>();
            iIdentity.SetupGet(x => x.IsAuthenticated).Returns(true);
            iIdentity.SetupGet(x => x.AuthenticationType).Returns("User");
            iIdentity.SetupGet(x => x.Name).Returns("TestName");

            var iUserInfo = iIdentity.As<IUserInfo>();
            iUserInfo.SetupGet(x => x.UserToken).Returns("TestToken");
            iUserInfo.SetupGet(x => x.UserId).Returns(userId.ToString());

            return iIdentity.Object;
        }

        public static IError GetMockError()
        {
            var error = new Mock<IError>();
            return error.Object;
        }
    }
}