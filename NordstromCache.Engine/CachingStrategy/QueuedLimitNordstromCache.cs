using System.Collections.Generic;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    /// <summary>
    /// This class just follows what a Queue does and implements it in Caching
    /// Basically, FIFO (First in First out)
    /// </summary>
    internal sealed class QueuedLimitNordstromCache : NordstromCacheBase
    {
        internal readonly Queue<CacheEntry> CacheEntryQueue;

        public QueuedLimitNordstromCache(int sizeLimit) : base(sizeLimit)
        {
            CacheEntryQueue = new Queue<CacheEntry>();
        }

        protected override void OnAdd(CacheEntry _, CacheEntry newEntry)
        {
            CacheEntryQueue.Enqueue(newEntry);
        }

        protected override void PerformEviction()
        {
            CacheEntryQueue.TryDequeue(out var dequeuedCacheEntry);
            Cache.Remove(dequeuedCacheEntry.Key);
        }
    }
}
