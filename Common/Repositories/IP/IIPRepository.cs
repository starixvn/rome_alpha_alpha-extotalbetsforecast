namespace Fanex.Razorbill.Module.Common.Repositories.IP
{
    using System.Collections.Generic;
    using Fanex.Razorbill.Module.Common.Entities;

    public interface IIPRepository
    {
        IList<IPRangeEntity> GetVNIPs();
    }
}