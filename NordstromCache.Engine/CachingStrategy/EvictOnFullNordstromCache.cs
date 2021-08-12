using System.Collections.Generic;
using System.Linq;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    internal sealed class EvictOnFullNordstromCache : NordstromCacheBase
    {
        internal readonly SortedDictionary<long, CacheEntry> CacheUsage;

        public EvictOnFullNordstromCache(int sizeLimit) : base(sizeLimit)
        {
            CacheUsage = new SortedDictionary<long, CacheEntry>();
        }

        protected override void OnAdd(CacheEntry oldEntry, CacheEntry newEntry)
        {
            CacheUsage[newEntry.LastUsedTicks] = newEntry;
            if (oldEntry != null) CacheUsage.Remove(oldEntry.LastUsedTicks);
        }

        protected override void OnGet(CacheEntry foundEntry)
        {
            CacheUsage[foundEntry.LastUsedTicks].UpdateLastUsed();
        }

        protected override void OnExist(CacheEntry foundEntry)
        {
            CacheUsage[foundEntry.LastUsedTicks].UpdateLastUsed();
        }

        protected override void PerformEviction()
        {
            var leastUsedCacheEntry = CacheUsage.ElementAt(0);
            Cache.Remove(leastUsedCacheEntry.Value.Key);
            CacheUsage.Remove(leastUsedCacheEntry.Key);
        }
    }
}
