using System.Collections.Generic;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    internal sealed class EvictOnFullNordstromCache : NordstromCacheBase
    {
        // The purpose of this is to have a built in way to say what's the last
        // and move something to the first to simulate what's the least and recently used cache
        internal readonly LinkedList<CacheEntry> CacheUsage;

        public EvictOnFullNordstromCache(int sizeLimit) : base(sizeLimit)
        {
            CacheUsage = new LinkedList<CacheEntry>();
        }

        protected override void OnAdd(CacheEntry oldEntry, CacheEntry newEntry)
        {
            CacheUsage.AddFirst(newEntry);
            if (oldEntry != null) CacheUsage.Remove(oldEntry);
        }

        protected override void OnGet(CacheEntry foundEntry)
        {
            ShiftToFirst(foundEntry);
        }

        private void ShiftToFirst(CacheEntry foundEntry)
        {
            CacheUsage.Remove(foundEntry);
            CacheUsage.AddFirst(foundEntry);
        }

        protected override void OnExist(CacheEntry foundEntry)
        {
            ShiftToFirst(foundEntry);
        }

        protected override void PerformEviction()
        {
            var leastUsedCache = CacheUsage.Last;
            CacheUsage.Remove(leastUsedCache.Value);
            Cache.Remove(leastUsedCache.Value.Key);
        }
    }
}
