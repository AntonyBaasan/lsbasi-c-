using System.Collections.Generic;

namespace Interpreter
{
    public class Token
    {
        public static Dictionary<string, Token> RESERVED_KEYWORDS = new Dictionary<string, Token>() {
            { "BEGIN", new Token(TokenType.BEGIN, "BEGIN")},
            { "END", new Token(TokenType.END, "END")}
        };

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
