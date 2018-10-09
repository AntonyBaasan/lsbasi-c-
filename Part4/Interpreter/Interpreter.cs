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

        public int Expr()
        {
            int result = int.Parse(Factor());

            while (currentToken.TokenType == TokenType.MUL || currentToken.TokenType == TokenType.DIV)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                    result *= int.Parse(Factor());
                }
                if (token.TokenType == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    result /= int.Parse(Factor());
                }
            }

            return result;
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private string Factor()
        {
            var token = currentToken;
            Eat(TokenType.INTEGER);
            return token.Value;
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

    }
}
