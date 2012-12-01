using System.Collections.Generic;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Tests.MockObjects;
using sBlog.Net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sBlog.Net.Tests.MockCollections
{
    [TestClass]
    public class ArchiveCollectionTests
    {
        [TestMethod]
        public void Can_Generate_Archive_Collection_With_Required_Month_And_Years()
        {
            IPost post = new MockPost();
            var mockArchives = GetTestArchives();
            var archiveCollection = new MockArchiveCollection(post.GetPosts());            
            foreach (var archive in mockArchives)
            {
                var archiveFromCollection = archiveCollection.Single(archive);
                Assert.IsNotNull(archiveFromCollection);
            }
        }

        private static IEnumerable<Archive> GetTestArchives()
        {
            var archives = new List<Archive>
                               {
                                   new Archive {Year = "2012", Month = "04", MonthYear = "April 2012"},
                                   new Archive {Year = "2012", Month = "01", MonthYear = "January 2012"}
                               };

            return archives;
        }
    }
}
