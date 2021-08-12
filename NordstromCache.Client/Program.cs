using System;
using NordstromCache.Engine;
using NordstromCache.Engine.Enums;

namespace NordstromCache.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var cacheFactory = new CacheFactory();
            cacheFactory.Get(EvictionMechanism.LastUsed, 10);
            Console.WriteLine("Hello World!");
        }
    }
}
