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

        private char Peek()
        {
            var peekPos = pos + 1;
            if (pos > text.Length - 1)
            {
                return char.MinValue;// indicate end of the input
            }
            else
            {
                return text[peekPos];
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

        private Token GetId()
        {
            var result = "";

            while (currentChar != char.MinValue && char.IsLetterOrDigit(currentChar))
            {
                result += currentChar;
                Advance();
            }

            if (Token.RESERVED_KEYWORDS.ContainsKey(result))
            {
                return Token.RESERVED_KEYWORDS[result];
            }

            return new Token(TokenType.ID, result);

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

                if (char.IsLetter(currentChar))
                {
                    return GetId();
                }

                if (char.IsDigit(currentChar))
                {
                    return new Token(TokenType.INTEGER, Integer().ToString());
                }

                if (currentChar == ':' && Peek() == '=')
                {
                    Advance();
                    Advance();
                    return new Token(TokenType.ASSIGN, ":=");
                }

                if (currentChar == ';')
                {
                    Advance();
                    return new Token(TokenType.SEMI, ";");
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

                if (currentChar == '(')
                {
                    Advance();
                    return new Token(TokenType.LPAREN, "(");
                }

                if (currentChar == ')')
                {
                    Advance();
                    return new Token(TokenType.RPAREN, ")");
                }

                if (currentChar == '.')
                {
                    Advance();
                    return new Token(TokenType.DOT, ".");
                }

                Error();
            }

            return new Token(TokenType.EOF, null);
        }

    }
}
