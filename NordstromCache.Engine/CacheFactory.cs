using System;
using System.Collections.Generic;
using NordstromCache.Engine.CachingStrategy;
using NordstromCache.Engine.Enums;
using NordstromCache.Engine.Interfaces;

namespace NordstromCache.Engine
{
    public sealed class CacheFactory : ICacheFactory
    {
        private static readonly Dictionary<EvictionMechanism, Func<int, INordstromCache>> _cacheStrategies;

        static CacheFactory()
        {
            // can use reflection to automatically get all types of caching, probably needs an attribute to map
            // the eviction mechanism to the correct type. duplicate check might be necessary
            // Can also use dependency injection, but since this is a factory class, for simplicity, use delegates
            _cacheStrategies = new Dictionary<EvictionMechanism, Func<int, INordstromCache>>
            {
                [EvictionMechanism.LastUsed] = sizeLimit => new EvictOnFullNordstromCache(sizeLimit),
                [EvictionMechanism.Queue] = sizeLimit => new QueuedLimitNordstromCache(sizeLimit)
            };
        }

        public INordstromCache Get(EvictionMechanism evictionMechanism, int sizeLimit)
        {
            if (sizeLimit <= 0) throw new InvalidOperationException($"SizeLimit of '{sizeLimit}' is invalid.");
            // could have some sort of exception handling
            return _cacheStrategies[evictionMechanism](sizeLimit);
        }
    }
}
