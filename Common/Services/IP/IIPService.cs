namespace Fanex.Razorbill.Module.Common.Services.IP
{
    using System.Collections.Generic;
    using Fanex.Razorbill.Module.Common.Entities;

    public interface IIPService
    {
        IList<IPRangeEntity> GetIPVN();
    }
}