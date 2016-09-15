using System.Configuration;

namespace SQlExecute.Models
{
  

    [ConfigurationCollection(typeof(TokenConfig))]
    public class TokenConfigs : ConfigurationElementCollection
    {        
        public TokenConfig this[int index]
        {
            get { return (TokenConfig)BaseGet(index); }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new TokenConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TokenConfig)element).TokenName;
        }
    }
}
