using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Infrastructure;
using sBlog.Net.Tests.Helpers;

namespace sBlog.Net.Tests.Generators
{
    [TestClass]
    public class UniqueUrlGeneratorTests
    {
        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_Url_1()
        {
            const string postTitle = "a test url 29";
            var postRepository = MockObjectFactory.CreatePostRepository();
            var generatedSlug = UniqueUrlHelper.FindUniqueUrl(postRepository, postTitle, 1);
            Assert.AreEqual("a-test-url-29", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_Url_1_Edit()
        {
            const string postTitle = "a test url 20";
            var postRepository = MockObjectFactory.CreatePostRepository();
            var generatedSlug = UniqueUrlHelper.FindUniqueUrl(postRepository, postTitle, 1, 20);
            Assert.AreEqual("a-test-url-20", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Non_Duplicate_Url_2()
        {
            const string postTitle = "a test url 20";
            var postRepository = MockObjectFactory.CreatePostRepository();
            var generatedSlug = UniqueUrlHelper.FindUniqueUrl(postRepository, postTitle, 1);
            Assert.AreEqual("a-test-url-20-2", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Non_Duplicate_Url_3()
        {
            const string postTitle = "programatic web.config";
            var postRepository = MockObjectFactory.CreatePostRepository();
            var generatedSlug = UniqueUrlHelper.FindUniqueUrl(postRepository, postTitle, 1);
            Assert.AreEqual("programatic-web-config", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Non_Duplicate_Url_4()
        {
            const string postTitle = "programatic web.config.config";
            var postRepository = MockObjectFactory.CreatePostRepository();
            var generatedSlug = UniqueUrlHelper.FindUniqueUrl(postRepository, postTitle, 1);
            Assert.AreEqual("programatic-web-config-config", generatedSlug);
        }
    }
}
