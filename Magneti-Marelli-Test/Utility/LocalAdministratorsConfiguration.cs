using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Magneti_Marelli_Test.Utility
{

    public class CustomConfigSection : ConfigurationSection
    {

        [ConfigurationProperty("ConfigElements", IsRequired = true)]
        public ConfigElementsCollection ConfigElements
        {
            get
            {
                return base["ConfigElements"] as ConfigElementsCollection;
            }
        }

    }

    [ConfigurationCollection(typeof(ConfigElement), AddItemName = "ConfigElement")]
    public class ConfigElementsCollection : ConfigurationElementCollection, IEnumerable<ConfigElement>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as ConfigElement;
            if (l_configElement != null)
                return l_configElement.Name;
            else
                return null;
        }

        public ConfigElement this[int index]
        {
            get
            {
                return BaseGet(index) as ConfigElement;
            }
        }

        #region IEnumerable<ConfigElement> Members

        IEnumerator<ConfigElement> IEnumerable<ConfigElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }

        #endregion
    }

    public class ConfigElement : ConfigurationElement
    {

        [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return base["Name"] as string;
            }
            set
            {
                base["Name"] = value;
            }
        }

        [ConfigurationProperty("Groups", IsRequired = true)]
        public string Groups
        {
            get
            {
                return base["Groups"] as string;
            }
            set
            {
                base["Groups"] = value;
            }
        }

        [ConfigurationProperty("OU", IsRequired = true)]
        public string OU
        {
            get
            {
                return base["OU"] as string;
            }
            set
            {
                base["OU"] = value;
            }
        }

    }




}