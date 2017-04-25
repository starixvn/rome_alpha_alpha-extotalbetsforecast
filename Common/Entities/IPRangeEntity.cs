namespace Fanex.Razorbill.Module.Common.Entities
{
    using System;

    /// <summary>
    /// IP Range Entity.
    /// </summary>
    [Serializable]
    public class IPRangeEntity
    {
        public double From { get; set; }

        public double To { get; set; }

        public bool IsManual { get; set; }
    }
}