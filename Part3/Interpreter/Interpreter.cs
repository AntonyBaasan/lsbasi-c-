using System;

namespace Interpreter
{
    public class Interpreter
    {
        private readonly string text;
        private int pos;
        private Token currentToken;
        private char currentChar;

        public Interpreter(string text)
        {
            this.text = text;
            pos = 0;
            currentToken = null;
            currentChar = text[pos];
        }

        public int Expr()
        {
            currentToken = GetNextToken();

            int result = int.Parse(Term());

            while (currentToken.TokenType == TokenType.PLUS || currentToken.TokenType == TokenType.MINUS)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.PLUS)
                {
                    Eat(TokenType.PLUS);
                    result += int.Parse(Term());
                }
                if (token.TokenType == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                    result -= int.Parse(Term());
                }
            }

            return result;
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private void Advance()
        {
            pos++;
            if (pos > text.Length - 1)
            {
                currentChar = char.MinValue;// indicate end of the input
            }
            else
            {
                currentChar = text[pos];
            }
        }

        private string Term()
        {
            var token = currentToken;
            Eat(TokenType.INTEGER);
            return token.Value;
        }

        private void SkipWhiteSpace()
        {
            while (currentChar != char.MinValue && char.IsWhiteSpace(currentChar))
            {
                Advance();
            }
        }

        private int Integer()
        {
            string result = "";
            while (currentChar != char.MinValue && char.IsDigit(currentChar))
            {
                result += currentChar;
                Advance();
            }

            return int.Parse(result);
        }

        private Token GetNextToken()
        {
            while (currentChar != char.MinValue)
            {
                if (char.IsWhiteSpace(currentChar))
                {
                    SkipWhiteSpace();
                    continue;
                }

                if (char.IsDigit(currentChar))
                {
                    return new Token(TokenType.INTEGER, Integer().ToString());
                }

                if (currentChar == '+')
                {
                    Advance();
                    return new Token(TokenType.PLUS, "+");
                }

                if (currentChar == '-')
                {
                    Advance();
                    return new Token(TokenType.MINUS, "-");
                }

                Error();
            }

            return new Token(TokenType.EOF, null);
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
