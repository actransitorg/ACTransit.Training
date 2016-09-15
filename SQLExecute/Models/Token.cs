namespace SQlExecute.Models
{
    public class Token
    {
        public Token(string name, string value)
        {
            TokenName = name;
            TokenValue = value;
        }
        public string TokenName { get; set; }
        public string TokenValue { get; set; }
    }
}
