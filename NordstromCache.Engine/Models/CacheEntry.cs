using System;

namespace NordstromCache.Engine.Models
{
    public class CacheEntry
    {
        public object Entry { get; }
        public DateTime LastUsed { get; private set; }

        public CacheEntry(object entry)
        {
            Entry = entry;
            UpdateLastUsed();
        }

        public void UpdateLastUsed()
        {
            LastUsed = DateTime.Now;
        }
    }
}
