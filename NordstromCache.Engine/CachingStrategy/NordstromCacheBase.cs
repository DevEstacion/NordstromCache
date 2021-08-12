﻿using System.Collections.Generic;
using NordstromCache.Engine.Interfaces;
using NordstromCache.Engine.Models;

namespace NordstromCache.Engine.CachingStrategy
{
    internal abstract class NordstromCacheBase : INordstromCache
    {
        private readonly int _sizeLimit;
        internal readonly Dictionary<object, CacheEntry> Cache;

        protected NordstromCacheBase(int sizeLimit)
        {
            _sizeLimit = sizeLimit;
            Cache = new Dictionary<object, CacheEntry>();
        }

        public void Add(object key, object value)
        {
            Cache.TryGetValue(key, out var oldEntry);
            var newEntry = new CacheEntry(key, value);
            Cache[key] = newEntry;
            OnAdd(oldEntry, newEntry);

            if (Cache.Count > _sizeLimit) PerformEviction();
        }

        public object Get(object key)
        {
            if (!Cache.TryGetValue(key, out var foundObj)) return null;
            OnGet(foundObj);
            return foundObj.Entry;
        }

        public bool Exist(object key)
        {
            if (!Cache.TryGetValue(key, out var foundObj)) return false;
            OnExist(foundObj);
            return true;
        }

        protected virtual void OnAdd(CacheEntry oldEntry, CacheEntry newEntry) { }
        protected virtual void OnGet(CacheEntry foundEntry) { }
        protected virtual void OnExist(CacheEntry foundEntry) { }
        protected abstract void PerformEviction();
    }
}
