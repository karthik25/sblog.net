using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Domain.Generators;

namespace sBlog.Net.Tests.Generators
{
    [TestClass]
    public class SlugGeneratorTests
    {
        [TestMethod]
        public void Can_Generate_Slug()
        {
            const string tag = "C#";
            var allSlugs = new List<string> {};
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate()
        {
            const string tag = "C$";
            var allSlugs = new List<string> {"c"};
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-2", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_2()
        {
            const string tag = "C@";
            var allSlugs = new List<string> { "c", "c-2" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-3", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_3()
        {
            const string tag = "C@";
            var allSlugs = new List<string> { "c", "c-2", "c-5" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-6", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_4()
        {
            const string tag = "C@";
            var allSlugs = new List<string> { "c", "mvc-3" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-2", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_New_Tag()
        {
            const string tag = "mvc 3";
            var allSlugs = new List<string> { };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("mvc-3", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_New_Tag_With_Period()
        {
            const string tag = "web.config";
            var allSlugs = new List<string> { };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("web-config", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_New_Tag_With_Period_2()
        {
            const string tag = "web.config";
            var allSlugs = new List<string> { "web-config" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("web-config-2", generatedSlug);
        }
        
        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_6()
        {
            const string tag = "mvc3";
            var allSlugs = new List<string> { "mvc3" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("mvc3-2", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_7()
        {
            const string tag = "mvc3";
            var allSlugs = new List<string> { "mvc-3" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("mvc3", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_8()
        {
            const string tag = "c          #";
            var allSlugs = new List<string> { };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-0", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Tag_With_Lot_Of_Spaces()
        {
            const string tag = "c          #";
            var allSlugs = new List<string> { "c-0" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-0-2", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Duplicate_9()
        {
            const string tag = "c%";
            var allSlugs = new List<string> { "c","c-2","mvc-3","mvc-3-2" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("c-3", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Text_With_Hyphen()
        {
            const string tag = "web-config";
            var allSlugs = new List<string> {  };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("web-config", generatedSlug);
        }

        [TestMethod]
        public void Can_Generate_Slug_For_Text_With_Hyphen_2()
        {
            const string tag = "web - config";
            var allSlugs = new List<string> { "web-config" };
            var generatedSlug = tag.GetUniqueSlug(allSlugs);
            Assert.AreEqual("web-config-2", generatedSlug);
        }
    }
}
