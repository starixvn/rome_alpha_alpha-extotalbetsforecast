namespace Fanex.Razorbill.Module.Common.Configurations.ProductConfigurations
{
    using System.Configuration;

    public class SportElement : EnableElement
    {
        [ConfigurationProperty("LiveTypes", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EnableElementCollection<LiveTypeElement>),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public EnableElementCollection<LiveTypeElement> LiveTypes
        {
            get
            {
                var liveTypes = (EnableElementCollection<LiveTypeElement>)base["LiveTypes"];
                return liveTypes;
            }
        }
    }
}