namespace Interpreter
{
    public class Token
    {
        public Token(TokenType type, string value)
        {
            TokenType = type;
            Value = value;
        }

        public TokenType TokenType { get; }

        public string Value { get; }

        public override string ToString()
        {
            return $"Token({TokenType.ToString()}, {Value}";
        }
    }
}
