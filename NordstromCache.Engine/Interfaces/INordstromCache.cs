namespace NordstromCache.Engine.Interfaces
{
    public interface INordstromCache
    {
        void Add(object key, object value);
        object Get(object key);
        bool Exist(object key);
    }
}
