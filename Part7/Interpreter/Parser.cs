using System;

namespace Interpreter
{
    public class Parser
    {
        private Token currentToken;
        private readonly Lexer lexer;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            currentToken = lexer.GetNextToken();
        }

        private void Error()
        {
            throw new Exception("Error input string");
        }

        private AST Factor()
        {
            var token = currentToken;
            if (token.TokenType == TokenType.INTEGER)
            {
                Eat(TokenType.INTEGER);
                return new Num(token);
            }
            else if (token.TokenType == TokenType.LPAREN)
            {
                Eat(TokenType.LPAREN);
                AST node = Expr();
                Eat(TokenType.RPAREN);
                return node;
            }
            
            return null;
        }

        private AST Term()
        {
            var node = Factor();

            while (currentToken.TokenType == TokenType.MUL || currentToken.TokenType == TokenType.DIV)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                }
                if (token.TokenType == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                }

                node = new BinOp(left: node, op: token, right: Factor());
            }

            return node;
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

        public AST Expr()
        {
            var node = Term();

            while (currentToken.TokenType == TokenType.PLUS || currentToken.TokenType == TokenType.MINUS)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.PLUS)
                {
                    Eat(TokenType.PLUS);
                }
                if (token.TokenType == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                }

                node = new BinOp(left: Term(), op: token, right: node);
            }

            return node;
        }

    }
}
