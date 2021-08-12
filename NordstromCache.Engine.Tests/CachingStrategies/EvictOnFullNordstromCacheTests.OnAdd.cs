using NordstromCache.Engine.CachingStrategy;
using Xunit;

namespace NordstromCache.Engine.Tests
{
    public partial class EvictOnFullNordstromCacheTests
    {
        [Fact]
        public void Add_OnAdd_AddCacheUsage()
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
            Assert.NotNull(cache.CacheUsage.Find(cacheEntry));

            // Cleanup
        }

        [Fact]
        public void Add_OnAdd_AddNewCacheEntry_MovePreviousToLast()
        {
            // Arrange
            var key1 = "foo_key1";
            var key2 = "foo_key2";
            var foo1 = new { a = 1 };
            var foo2 = new { a = 1 };
            var cache = new EvictOnFullNordstromCache(10);

            cache.Add(key1, foo1);

            // Act
            cache.Add(key2, foo2);

            // Assert
            Assert.Same(cache.Cache[key2], cache.CacheUsage.First.Value);
            Assert.Same(cache.Cache[key1], cache.CacheUsage.Last.Value);

            // Cleanup
        }


        [Fact]
        public void Add_OnAdd_AddManyCacheEntries_CheckFirstAndLast()
        {
            // Arrange
            var firstEnteredKey = "firstEnteredKey";
            var lastEnteredKey = "lastEnteredKey";
            var cache = new EvictOnFullNordstromCache(100);

            cache.Add(firstEnteredKey, new { first = true });
            for (var i = 0; i < 80; i++) cache.Add(i, new { index = i });

            // Act
            cache.Add(lastEnteredKey, new { lastEnteredKey = true });


            // Assert
            Assert.Same(cache.Cache[lastEnteredKey], cache.CacheUsage.First.Value);
            Assert.Same(cache.Cache[firstEnteredKey], cache.CacheUsage.Last.Value);

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
            Assert.NotNull(cache.CacheUsage.Find(cacheEntry));

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
            Assert.NotNull(cache.CacheUsage.Find(cacheEntry2));

            // Cleanup
        }
    }
}
