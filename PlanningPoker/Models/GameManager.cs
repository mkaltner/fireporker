using System;
using System.Runtime.Caching;

namespace PlanningPoker.Models
{
    public class GameManager
    {
        private const string CachePrefix = "PokerGame";
        private static readonly object cacheLock = new object();

        public static void StorePokerGame(PokerGame game)
        {
            var cacheKey = string.Format("{0}_{1}", CachePrefix, game.Id);
            StoreCacheObject(cacheKey, game);
        }

        public static PokerGame GetPokerGame(Guid gameId)
        {
            var cacheKey = string.Format("{0}_{1}", CachePrefix, gameId);
            var game = GetCacheObject<PokerGame>(cacheKey);
            return game;
        }

        private static void StoreCacheObject(string cacheKey, object cacheObject)
        {
            lock (cacheLock)
            {
                MemoryCache.Default.Set(cacheKey, cacheObject, DateTime.Today.AddDays(1));
            }
        }

        private static T GetCacheObject<T>(string cacheKey) where T : class
        {
            var cachedObject = MemoryCache.Default.Get(cacheKey, null) as T;

            if (cachedObject != null)
            {
                return cachedObject;
            }

            lock (cacheLock)
            {
                //Check to see if anyone wrote to the cache while we where waiting our turn to write the new value.
                cachedObject = MemoryCache.Default.Get(cacheKey, null) as T;

                if (cachedObject != null)
                {
                    return cachedObject;
                }
            }

            return null;
        }
    }
}