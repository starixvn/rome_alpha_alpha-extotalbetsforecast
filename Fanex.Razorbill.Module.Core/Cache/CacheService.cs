namespace Fanex.Razorbill.Module.Core.Cache
{
    using System;
    using System.Runtime.Caching;

    public static class CachingService
    {
        private static volatile ObjectCache _cacheManager;

        private static object _syncRoot = new object();

        public static ObjectCache CacheManager
        {
            get
            {
                if (_cacheManager == null)
                {
                    lock (_syncRoot)
                    {
                        if (_cacheManager == null)
                        {
                            _cacheManager = MemoryCache.Default;
                        }
                    }
                }

                return _cacheManager;
            }
        }

        public static T GetOrAdd<T>(string key, Func<T> addItem, int expiredSeconds = 30)
        {
            var value = CacheManager.Get(key);

            if (value == null)
            {
                value = addItem();

                CacheManager.Set(
                    key,
                    value,
                    new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(expiredSeconds)
                    });
            }

            if (value is T)
            {
                return (T)value;
            }

            return default(T);
        }
    }
}