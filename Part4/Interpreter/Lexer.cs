using System;

namespace Interpreter
{
    public class Lexer
    {
        private readonly string text;
        private int pos;
        private char currentChar;

        public Lexer(string text)
        {
            this.text = text;
            pos = 0;
            currentChar = text[pos];
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

        public Token GetNextToken()
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

                if (currentChar == '*')
                {
                    Advance();
                    return new Token(TokenType.MUL, "*");
                }

                if (currentChar == '/')
                {
                    Advance();
                    return new Token(TokenType.DIV, "/");
                }

                Error();
            }

            return new Token(TokenType.EOF, null);
        }

    }
}
