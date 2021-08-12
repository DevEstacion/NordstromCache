namespace NordstromCache.Engine.Models
{
    internal sealed class CacheEntry
    {
        public CacheEntry(object key, object entry)
        {
            Key = key;
            Entry = entry;
        }

        public object Key { get; }
        public object Entry { get; }
    }
}
