using System;
using System.Collections.Generic;

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

        private AST Program()
        {
            AST node = CompoundStatement();
            Eat(TokenType.DOT);
            return node;
        }

        private AST CompoundStatement()
        {
            // compound_statement: BEGIN statement_list END
            Eat(TokenType.BEGIN);
            List<AST> nodes = StatementList();
            Eat(TokenType.END);

            Compound root = new Compound();
            foreach (var node in nodes)
            {
                root.Children.Add(node);
            }

            return root;
        }

        private List<AST> StatementList()
        {
            // statement_list: statement | statement SEMI statement_list
            AST node = Statement();
            var results = new List<AST> { node };

            while (currentToken.TokenType == TokenType.SEMI)
            {
                Eat(TokenType.SEMI);
                results.Add(Statement());
            }
            if (currentToken.TokenType == TokenType.ID)
            {
                Error();
            }

            return results;
        }

        private AST Statement()
        {
            // statement: compound_statement | assignment_statement | empty
            AST node = null;
            if (currentToken.TokenType == TokenType.BEGIN)
            {
                node = CompoundStatement();
            }
            else if (currentToken.TokenType == TokenType.ID)
            {
                node = AssignmentStatement();
            }
            else
            {
                node = EmptyNode();
            }

            return node;
        }

        public AST AssignmentStatement()
        {
            // assignment_statement : variable ASSIGN expr
            var left = Variable();
            var token = currentToken;
            Eat(TokenType.ASSIGN);
            var right = Expr();
            var node = new Assign(left, token, right);
            return node;
        }

        private Var Variable()
        {
            var node = new Var(currentToken);
            Eat(TokenType.ID);
            return node;
        }

        private AST EmptyNode()
        {
            return new NoOp();
        }

        private AST Factor()
        {
            /* factor : PLUS factor
                  | MINUS factor
                  | INTEGER
                  | LPAREN expr RPAREN
                  | variable
            */
            var token = currentToken;
            if (token.TokenType == TokenType.PLUS)
            {
                Eat(TokenType.PLUS);
                return new UnaryOp(token, Factor());
            }
            else if (token.TokenType == TokenType.MINUS)
            {
                Eat(TokenType.MINUS);
                return new UnaryOp(token, Factor());
            }
            else if (token.TokenType == TokenType.INTEGER)
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
            else
            {
                return Variable();
            }
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
                currentToken = lexer.GetNextToken();
            }
            else
            {
                Error();
            }
        }

        public AST Expr()
        {
            // expr : term ((PLUS | MINUS) term)*

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

                node = new BinOp(left: node, op: token, right: Term());
            }

            return node;
        }

        public AST Parse()
        {
            var node = Program();
            if (currentToken.TokenType != TokenType.EOF)
            {
                Error();
            }
            return node;
        }

    }
}
