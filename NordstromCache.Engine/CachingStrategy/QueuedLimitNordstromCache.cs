using System.Collections.Generic;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    internal sealed class QueuedLimitNordstromCache : NordstromCacheBase
    {
        private readonly Queue<CacheEntry> _cacheEntryQueue;

        public QueuedLimitNordstromCache(int sizeLimit) : base(sizeLimit)
        {
            _cacheEntryQueue = new Queue<CacheEntry>();
        }

        protected override void OnAdd(CacheEntry _, CacheEntry newEntry)
        {
            _cacheEntryQueue.Enqueue(newEntry);
        }

        protected override void PerformEviction()
        {
            _cacheEntryQueue.TryDequeue(out _);
        }
    }
}
