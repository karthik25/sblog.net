using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Domain.Hashers;

namespace sBlog.Net.Tests.Hasher
{
    [TestClass]
    public class HasherTests
    {
        [TestMethod]
        public void Can_Generate_The_Expected_Hasher()
        {
            var hasher = Net.Infrastructure.Hasher.Instance;
            Assert.IsNotNull(hasher);
            Assert.IsInstanceOfType(hasher, typeof(Md5Hasher));
        }
    }
}
