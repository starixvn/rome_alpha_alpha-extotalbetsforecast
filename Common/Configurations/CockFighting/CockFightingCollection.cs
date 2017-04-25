namespace Fanex.Razorbill.Module.Common.Configurations.CockFighting
{
    using System.Configuration;

    public class CockFightingCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public CockFightingElement this[int index]
        {
            get
            {
                return (CockFightingElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        public new CockFightingElement this[string elementKey]
        {
            get
            {
                return (CockFightingElement)BaseGet(elementKey);
            }
        }

        public void Add(CockFightingElement element)
        {
            BaseAdd(element);
        }

        public void Remove(CockFightingElement element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.Key);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string alias)
        {
            BaseRemove(alias);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(CockFightingElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement kenoBetTypesElement)
        {
            BaseAdd(kenoBetTypesElement, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new CockFightingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CockFightingElement)element).Key;
        }
    }
}
