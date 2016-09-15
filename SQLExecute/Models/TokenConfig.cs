using System.Configuration;

namespace SQlExecute.Models
{
    public class TokenConfig: ConfigurationSection
    {
        const string Key = "TokenName";
        const string Value = "TokenValue";
        [ConfigurationProperty(Key, DefaultValue = "", IsRequired = true, IsKey = true)]
        public string TokenName
        {
            get
            {
                return (string)base[Key];
            }
            set { base[Key] = value; }
        }

        [ConfigurationProperty(Value, DefaultValue = "", IsRequired = true)]
        public string TokenValue
        {
            get
            {
                return (string)base[Value];
            }
            set { base[Value] = value; }
        }
    }
}
