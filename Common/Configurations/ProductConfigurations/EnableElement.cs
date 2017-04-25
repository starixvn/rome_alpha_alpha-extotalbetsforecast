namespace Fanex.Razorbill.Module.Common.Configurations.ProductConfigurations
{
    using System.Configuration;
    using Fanex.Common;

    public class EnableElement : ConfigurationElement
    {
        [ConfigurationProperty("id", DefaultValue = "0", IsRequired = true, IsKey = true)]
        public string Id
        {
            get
            {
                return Converter.ToString2(this["id"]);
            }

            set
            {
                this["id"] = value;
            }
        }

        [ConfigurationProperty("active", DefaultValue = true, IsRequired = false, IsKey = true)]
        public bool Active
        {
            get
            {
                return Converter.ToBool(this["active"]);
            }

            set
            {
                this["active"] = value;
            }
        }

        [ConfigurationProperty("name", DefaultValue = "NA", IsRequired = false, IsKey = false)]
        public string Name
        {
            get
            {
                return Converter.ToString2(this["name"]);
            }

            set
            {
                this["name"] = value;
            }
        }
    }
}