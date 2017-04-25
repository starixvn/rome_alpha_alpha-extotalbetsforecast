namespace Fanex.Razorbill.Module.Common.Configurations.ProductConfigurations
{
    using System.Configuration;

    public class LiveTypeElement : EnableElement
    {
        [ConfigurationProperty("BetTypes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EnableElementCollection<BetTypeElement>),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public EnableElementCollection<BetTypeElement> BetTypes
        {
            get
            {
                var betTypes = (EnableElementCollection<BetTypeElement>)base["BetTypes"];
                return betTypes;
            }
        }
    }
}