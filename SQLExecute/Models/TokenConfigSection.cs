using System.Configuration;

namespace SQlExecute.Models
{
    public class TokenConfigSection : ConfigurationSection
    {

        [ConfigurationProperty("Tokens")]
        public TokenConfigs Tokens
        {
            get { return ((TokenConfigs)(base["Tokens"])); }
        }
    }
}
