namespace Fanex.Razorbill.Module.Common.Configurations.ProductConfigurations
{
    using System.Configuration;

    public class EnableElementCollection<T> : ConfigurationElementCollection
        where T : EnableElement, new()
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public T this[int index]
        {
            get
            {
                return (T)BaseGet(index);
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

        public new T this[string elementKey]
        {
            get
            {
                return (T)BaseGet(elementKey);
            }
        }

        public void Add(T element)
        {
            BaseAdd(element);
        }

        public void Remove(T element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.Name);
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

        public int IndexOf(T element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Name;
        }
    }
}