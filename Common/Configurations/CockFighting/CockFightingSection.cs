namespace Fanex.Razorbill.Module.Common.Configurations.CockFighting
{
    using System.Configuration;

    public class CockFightingSection : ConfigurationSection
    {
        public static CockFightingSection Instance
        {
            get
            {
                var section = ConfigurationManager.GetSection("CockFightingSection") as CockFightingSection;
                return section;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(CockFightingCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public CockFightingCollection Configurations
        {
            get
            {
                return (CockFightingCollection)base["Configurations"];
            }
        }
    }
}
