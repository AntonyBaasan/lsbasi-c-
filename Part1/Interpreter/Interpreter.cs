using System;

namespace Interpreter
{
    public class Interpreter
    {
        private readonly string text;
        private int pos;
        private Token currentToken;

        public Interpreter(string text)
        {
            this.text = text;
            pos = 0;
            currentToken = null;
        }

        public int Expr()
        {
            currentToken = GetNextToken();

            var left = currentToken;
            Eat(TokenType.INTEGER);

            var op = currentToken;
            Eat(TokenType.PLUS);

            var right = currentToken;
            Eat(TokenType.INTEGER);

            return int.Parse(left.Value) + int.Parse(right.Value);
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private Token GetNextToken()
        {
            var text = this.text;

            if (pos > text.Length - 1)
                return new Token(TokenType.EOF, null);

            var currentChar = text[pos];

            if (char.IsDigit(currentChar))
            {
                var token = new Token(TokenType.INTEGER, currentChar.ToString());
                pos++;
                return token;
            }

            if (currentChar == '+')
            {
                var token = new Token(TokenType.PLUS, currentChar.ToString());
                pos++;
                return token;
            }

            Error();
            return null;
        }

        private void Eat(TokenType tokenType)
        {
            if (currentToken.TokenType == tokenType)
            {
                currentToken = GetNextToken();
            }
            else
            {
                Error();
            }

        }


    }
}
