namespace Fanex.Razorbill.Module.Common.Services
{
    using Fanex.Caching;
    using Fanex.Caching.Memory;

    public static class CacheFactory
    {       
        public static ICacheService MemoryCacheService { get; } = new CacheService(new MemoryCache(new MemoryCacheOptions()));
    }
}