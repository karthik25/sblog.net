using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Akismet;

namespace sBlog.Net.Tests.AkismetTests
{
    [TestClass]
    public class AkismetTest {

        const string key = "8b8d8d8c375d";
        const string blog = "http://your.url.here.com";

        Akismet.Akismet api;

        [TestInitialize]
        public void Setup() 
        {
             api = new Akismet.Akismet(key, blog, null);
        }

        [TestMethod]
        public void Verification() 
        {
            Assert.IsTrue(api.VerifyKey(), "VerifyKey() returned 'False' when 'True' was expected.");
        }

        [TestMethod]
        public void SpamTest() 
        {            
            var comment = new AkismetComment
                {
                    Blog = blog,
                    UserIp = "147.202.45.202",
                    UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)",
                    CommentContent =
                        "A friend of mine told me about this place. I'm just wondering if this thing works.   You check this posts everyday incase <a href=\"http://someone.finderinn.com\">find someone</a> needs some help?  I think this is a great job! Really nice place http://someone.finderinn.com here.   I found a lot of interesting stuff all around.  I enjoy beeing here and i'll come back soon. Many greetings.",
                    CommentType = "blog",
                    CommentAuthor = "someone",
                    CommentAuthorEmail = "backthismailtojerry@fastmail.fm",
                    CommentAuthorUrl = "http://someone.finderinn.com"
                };

            bool result = api.CommentCheck(comment);

            Assert.IsTrue(result, "API was expected to return 'True' when 'False' was returned instead.");
        }

        [TestMethod]
        public void NonSpamTest() 
        {
            var comment = new AkismetComment
                {
                    Blog = blog,
                    UserIp = "127.0.0.1",
                    UserAgent =
                        "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
                    CommentContent = "Hey, I'm testing out the Akismet API!",
                    CommentType = "blog",
                    CommentAuthor = "Joel",
                    CommentAuthorEmail = "",
                    CommentAuthorUrl = ""
                };

            bool result = api.CommentCheck(comment);

            Assert.IsFalse(result, "API was expected to return 'False' when 'True' was returned instead.");
        }

        [Ignore]
        [TestMethod]
        public void SubmitSpam()
        {
            var comment = new AkismetComment
                {
                    Blog = blog,
                    UserIp = "147.202.45.202",
                    UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)",
                    CommentContent =
                        "A friend of mine told me about this place. I'm just wondering if this thing works.   You check this posts everyday incase <a href=\"http://someone.finderinn.com\">find someone</a> needs some help?  I think this is a great job! Really nice place http://someone.finderinn.com here.   I found a lot of interesting stuff all around.  I enjoy beeing here and i'll come back soon. Many greetings.",
                    CommentType = "blog",
                    CommentAuthor = "someone",
                    CommentAuthorEmail = "backthismailtojerry@fastmail.fm",
                    CommentAuthorUrl = "http://someone.finderinn.com"
                };

            api.SubmitSpam(comment);
        }

        [Ignore]
        [TestMethod]
        public void SubmitHam() 
        {
            var comment = new AkismetComment
                {
                    Blog = blog,
                    UserIp = "127.0.0.1",
                    UserAgent =
                        "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
                    CommentContent = "Hey, I'm testing out the Akismet API!",
                    CommentType = "blog",
                    CommentAuthor = "Joel",
                    CommentAuthorEmail = "",
                    CommentAuthorUrl = ""
                };

            api.SubmitHam(comment);
        }
    }
}
