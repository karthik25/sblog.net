using System.Linq;
using sBlog.Net.Collections;
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockArchiveCollection : ArchiveCollection
    {
        public MockArchiveCollection(List<PostEntity> postsList)
            : base(postsList)
        {

        }

        public Archive Single(Archive archive)
        {
            return Archives.SingleOrDefault(a => a.Month == archive.Month && 
                                                 a.Year == archive.Year && 
                                                 a.MonthYear == archive.MonthYear);
        }
    }
}
