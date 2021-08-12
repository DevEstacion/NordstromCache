using System;

namespace NordstromCache.Engine.Models
{
    internal sealed class CacheEntry
    {
        public CacheEntry(object key, object entry)
        {
            Key = key;
            Entry = entry;
            UpdateLastUsed();
        }

        public object Key { get; }
        public object Entry { get; }
        public long LastUsedTicks { get; private set; }

        public void UpdateLastUsed()
        {
            LastUsedTicks = DateTime.Now.Ticks;
        }
    }
}
