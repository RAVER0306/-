using System.Configuration;

namespace Shine.Core.Configs.ConfigFile
{
    internal class ShineFrameworkSection : ConfigurationSection
    {
        private const string XmlnsKey = "xmlns";
        private const string DataKey = "data";
        private const string LoggingKey = "logging";

        [ConfigurationProperty(XmlnsKey, IsRequired = false)]
        private string Xmlns
        {
            get { return (string)this[XmlnsKey]; }
            set { this[XmlnsKey] = value; }
        }

        [ConfigurationProperty(DataKey)]
        public virtual DataElement Data
        {
            get { return (DataElement)this[DataKey]; }
            set { this[DataKey] = value; }
        }

        [ConfigurationProperty(LoggingKey)]
        public virtual LoggingElement Logging
        {
            get { return (LoggingElement)this[LoggingKey]; }
            set { this[LoggingKey] = value; }
        }
    }
}