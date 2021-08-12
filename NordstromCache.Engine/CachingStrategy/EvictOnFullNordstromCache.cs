using System.Collections.Generic;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    internal sealed class EvictOnFullNordstromCache : NordstromCacheBase
    {
        private readonly SortedDictionary<long, CacheEntry> _cacheUsage;

        public EvictOnFullNordstromCache(int sizeLimit) : base(sizeLimit)
        {
            _cacheUsage = new SortedDictionary<long, CacheEntry>();
        }

        protected override void OnAdd(CacheEntry oldEntry, CacheEntry newEntry)
        {
            _cacheUsage[newEntry.LastUsed.Ticks] = newEntry;
            _cacheUsage.Remove(oldEntry.LastUsed.Ticks);
        }

        protected override void OnGet(CacheEntry foundEntry)
        {
            _cacheUsage[foundEntry.LastUsed.Ticks].UpdateLastUsed();
        }

        protected override void OnExist(CacheEntry foundEntry)
        {
            _cacheUsage[foundEntry.LastUsed.Ticks].UpdateLastUsed();
        }

        protected override void PerformEviction()
        {
            var leastUsedCacheEntry = _cacheUsage[0];
            Cache.Remove(leastUsedCacheEntry);
        }
    }
}
