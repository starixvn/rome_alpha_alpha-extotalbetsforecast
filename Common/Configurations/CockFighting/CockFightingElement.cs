namespace Fanex.Razorbill.Module.Common.Configurations.CockFighting
{
    using System.Configuration;
    using Fanex.Common;

    public class CockFightingElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true, IsRequired = true, DefaultValue = "")]
        public string Key
        {
            get
            {
                return Converter.ToString2(this["key"]);
            }

            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = true, DefaultValue = "")]
        public string Value
        {
            get
            {
                return Converter.ToString2(this["value"]);
            }

            set
            {
                this["value"] = value;
            }
        }
    }
}
