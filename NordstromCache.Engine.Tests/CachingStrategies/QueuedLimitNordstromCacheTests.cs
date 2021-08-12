using System.Linq;
using NordstromCache.Engine.CachingStrategy;
using Xunit;

namespace NordstromCache.Engine.Tests
{
    public class QueuedLimitNordstromCacheTests
    {
        [Fact]
        public void Add_OnAdd_QueueCacheEntry()
        {
            // Arrange
            var key = "foo_key";
            var foo = new { i = 1 };
            var cache = new QueuedLimitNordstromCache(10);

            // Act
            cache.Add(key, foo);

            // Assert
            Assert.Single(cache.Cache);
            Assert.Single(cache.CacheEntryQueue);

            // Cleanup
        }

        [Fact]
        public void Add_OnAdd_QueueCacheEntryFull_DequeueFirstEntry()
        {
            // Arrange
            var key1 = "foo_key1";
            var key2 = "foo_key2";
            var foo1 = new { i = 1 };
            var foo2 = new { i = 2 };
            var cache = new QueuedLimitNordstromCache(1);
            cache.Add(key1, foo1);

            // Act
            cache.Add(key2, foo2);

            // Assert
            Assert.Single(cache.Cache);
            Assert.Single(cache.CacheEntryQueue);
            Assert.False(cache.Cache.TryGetValue(key1, out _));
            Assert.Null(cache.CacheEntryQueue.SingleOrDefault(x => x.Key == key1));

            // Cleanup
        }
    }
}
