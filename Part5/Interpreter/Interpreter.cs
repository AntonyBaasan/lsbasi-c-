using System;

namespace Interpreter
{
    public class Interpreter
    {
        private Token currentToken;
        private readonly Lexer lexer;

        public Interpreter(Lexer lexer)
        {
            this.lexer = lexer;
            currentToken = lexer.GetNextToken();
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private int Factor()
        {
            var token = currentToken;
            Eat(TokenType.INTEGER);
            return int.Parse(token.Value);
        }

        private int Term()
        {
            var result = Factor();

            while (currentToken.TokenType == TokenType.MUL || currentToken.TokenType == TokenType.DIV)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                    result *= Factor();
                }
                if (token.TokenType == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    result /= Factor();
                }
            }

            return result;
        }

        private void Eat(TokenType tokenType)
        {
            if (currentToken.TokenType == tokenType)
            {
                currentToken = this.lexer.GetNextToken();
            }
            else
            {
                Error();
            }
        }

        public int Expr()
        {
            int result = Term();

            while (currentToken.TokenType == TokenType.PLUS || currentToken.TokenType == TokenType.MINUS)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.PLUS)
                {
                    Eat(TokenType.PLUS);
                    result += Term();
                }
                if (token.TokenType == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                    result -= Term();
                }
            }

            return result;
        }

    }
}
