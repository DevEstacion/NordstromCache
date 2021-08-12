using System;
using NordstromCache.Engine.CachingStrategy;
using Xunit;

namespace NordstromCache.Engine.Tests
{
    public class EvictOnFullNordstromCacheTests
    {
        [Fact]
        public void Add_OnAdd_AddCacheUsageUsingCacheEntryTicks()
        {
            // Arrange
            var key = "foo_key";
            var foo = new { a = 1 };
            var cache = new EvictOnFullNordstromCache(10);

            // Act
            cache.Add(key, foo);

            // Assert
            var cacheEntry = cache.Cache[key];
            Assert.Same(cacheEntry.Entry, foo);
            Assert.NotNull(cache.CacheUsage[cacheEntry.LastUsedTicks]);

            // Cleanup
        }

        [Fact]
        public void Add_OnAdd_RemoveOldEntryIfOverwritten()
        {
            // Arrange
            var key = "foo_key";
            var foo1 = new { a = 1 };
            var foo2 = new { a = 2 };
            var cache = new EvictOnFullNordstromCache(10);

            // Act
            cache.Add(key, foo1);
            cache.Add(key, foo2);

            // Assert
            Assert.Single(cache.Cache);
            var cacheEntry = cache.Cache[key];
            Assert.Same(cacheEntry.Entry, foo2);
            Assert.Single(cache.CacheUsage);
            Assert.NotNull(cache.CacheUsage[cacheEntry.LastUsedTicks]);

            // Cleanup
        }

        [Fact]
        public void Add_OnAdd_OnMeetSizePerformEviction()
        {
            // Arrange
            var key1 = "foo_key1";
            var key2 = "foo_key2";
            var foo1 = new { a = 1 };
            var foo2 = new { a = 2 };
            var cache = new EvictOnFullNordstromCache(1);

            // Act
            cache.Add(key1, foo1);
            cache.Add(key2, foo2);

            // Assert
            Assert.Single(cache.Cache);
            var cacheEntry2 = cache.Cache[key2];
            Assert.Same(cacheEntry2.Entry, foo2);
            Assert.False(cache.Cache.TryGetValue(key1, out _));
            Assert.Single(cache.CacheUsage);
            Assert.NotNull(cache.CacheUsage[cacheEntry2.LastUsedTicks]);

            // Cleanup
        }
    }
}
