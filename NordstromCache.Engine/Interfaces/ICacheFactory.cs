using NordstromCache.Engine.Enums;

namespace NordstromCache.Engine.Interfaces
{
    public interface ICacheFactory
    {
        INordstromCache Get(EvictionMechanism evictionMechanism, int sizeLimit);
    }
}
