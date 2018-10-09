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

            var left = currentToken;
            Eat(TokenType.INTEGER);

            var op = currentToken;
            if(op.TokenType == TokenType.PLUS)
                Eat(TokenType.PLUS);
            else
                Eat(TokenType.MINUS);

            var right = currentToken;
            Eat(TokenType.INTEGER);


            if(op.TokenType == TokenType.PLUS)
                return int.Parse(left.Value) + int.Parse(right.Value);
            else
                return int.Parse(left.Value) - int.Parse(right.Value);
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private void Advance()
        {
            pos++; ;
            if (pos > text.Length - 1)
            {
                currentChar = char.MinValue;// indicate end of the input
            }
            else
            {
                currentChar = text[pos];
            }
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
