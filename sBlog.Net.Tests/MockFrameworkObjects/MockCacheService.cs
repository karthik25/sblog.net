using System;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockCacheService : ICacheService
    {
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            var item = getItemCallback();
            return item;
        }
    }
}
