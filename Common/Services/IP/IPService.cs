namespace Fanex.Razorbill.Module.Common.Services.IP
{
    using System;
    using System.Collections.Generic;
    using Fanex.Caching;
    using Fanex.Razorbill.Module.Common.Entities;
    using Fanex.Razorbill.Module.Common.Repositories.IP;

    /// <summary>
    /// IP Services.
    /// </summary>
    public class IPService : IIPService
    {
        private IIPRepository _repository;

        public IPService() : this(null)
        {
        }

        public IPService(IPRepository repository)
        {
            _repository = repository ?? new IPRepository();
        }

        /// <summary>
        /// Get IP VN.
        /// </summary>
        /// <returns>Return result.</returns>
        public IList<IPRangeEntity> GetIPVN()
        {
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            List<IPRangeEntity> ipsFromCaching = cache.Get<List<IPRangeEntity>>(Constants.SessionKey.VietnamIPList);
            if (ipsFromCaching != null && ipsFromCaching.Count > 0)
            {
                return ipsFromCaching;
            }

            var ipsFromDB = _repository.GetVNIPs();
            cache.Set<IList<IPRangeEntity>>(Constants.SessionKey.VietnamIPList, ipsFromDB, TimeSpan.FromHours(1.0));

            return ipsFromDB;
        }
    }
}