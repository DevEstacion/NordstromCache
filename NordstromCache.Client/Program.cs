using System;
using NordstromCache.Engine;
using NordstromCache.Engine.Enums;
using NordstromCache.Engine.Interfaces;

namespace NordstromCache.Client
{
    class Program
    {
        private static readonly CacheFactory _cacheFactory;
        private static readonly INordstromCache _cache;

        static Program()
        {
            /*could use a fancy IOC container, abstract the registration to a Startup class
            and then register the Factory to the container. Inject it to all dependent classes as necessary
            for simplicity we'll just put this here*/
            _cacheFactory = new CacheFactory();
            _cache = _cacheFactory.Get(EvictionMechanism.LastUsed, 10);
        }

        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                _cache.Add(arg, new { arg, timeStamp = DateTime.Now.Ticks });
            }

            Console.ReadKey();
        }
    }
}
