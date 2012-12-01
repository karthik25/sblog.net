using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Tests.MockObjects;
using sBlog.Net.Akismet.Interfaces;
using sBlog.Net.Tests.MockFrameworkObjects;

namespace sBlog.Net.Tests.Helpers
{
    internal class MockObjectFactory
    {
        public static IPost CreatePostRepository()
        {
            return new MockPost();
        }

        public static IUser CreateUserRepository()
        {
            return new MockUser();
        }

        public static IComment CreateCommentRepository()
        {
            return new MockComment();
        }

        public static ICategory CreateCategoryRepository()
        {
            return new MockCategory();
        }

        public static ITag CreateTagRepository()
        {
            return new MockTag();
        }

        public static ISettings CreateSettingsRepository()
        {
            return new MockSettings();
        }

        public static ISettings CreateSettingsRepository(int loadType)
        {
            return new MockSettings(loadType);
        }

        public static IAkismetService CreateAkismetService()
        {
            return new MockAkismetService(null, null, null);
        }

        public static ICacheService CreateCacheService()
        {
            return new MockCacheService();
        }

        public static IError CreateErrorLogger()
        {
            return new MockError();
        }
    }
}
