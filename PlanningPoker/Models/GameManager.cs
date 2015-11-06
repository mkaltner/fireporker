using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace PlanningPoker.Models
{
    public class GameManager
    {
        private const string CachePrefix = "PokerGame";
        private static readonly object cacheLock = new object();
        private static List<string> _cacheKeys = new List<string>();

        public static int GameCount { get { return _cacheKeys.Count; } }

        public static void StorePokerGame(PokerGame game)
        {
            var cacheKey = string.Format("{0}_{1}", CachePrefix, game.Id);
            StoreCacheObject(cacheKey, game, game.ExpirationDate);
            if (!_cacheKeys.Contains(cacheKey))
                _cacheKeys.Add(cacheKey);
        }
        
        public static PokerGame GetPokerGame(Guid gameId)
        {
            var cacheKey = string.Format("{0}_{1}", CachePrefix, gameId);
            var game = GetCacheObject<PokerGame>(cacheKey);
            return game;
        }

        private static void StoreCacheObject(string cacheKey, object cacheObject, DateTime expirationDate)
        {
            lock (cacheLock)
            {
                MemoryCache.Default.Set(cacheKey, cacheObject, expirationDate);
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