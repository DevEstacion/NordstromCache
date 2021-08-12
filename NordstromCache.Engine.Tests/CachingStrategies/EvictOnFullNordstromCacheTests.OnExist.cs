using System;
using NordstromCache.Engine.CachingStrategy;
using Xunit;

namespace NordstromCache.Engine.Tests
{
    public partial class EvictOnFullNordstromCacheTests
    {
        [Fact]
        public void Exist_OnExist_ShiftListOrder()
        {
            // Arrange
            var key1 = "foo_key1";
            var key2 = "foo_key2";
            var foo1 = new { a = 1 };
            var foo2 = new { a = 1 };
            var cache = new EvictOnFullNordstromCache(10);

            cache.Add(key1, foo1);
            cache.Add(key2, foo2);

            // Act
            _ = cache.Get(key1);

            // Assert
            Assert.Same(cache.Cache[key1], cache.CacheUsage.First.Value);
            Assert.Same(cache.Cache[key2], cache.CacheUsage.Last.Value);

            // Cleanup
        }
    }
}
