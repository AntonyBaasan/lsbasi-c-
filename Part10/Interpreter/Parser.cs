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
            Eat(TokenType.PROGRAM);

            var varNode = Variable();
            var progName = varNode.Value;

            Eat(TokenType.SEMI);

            var blockNode = Block();
            var programNode = new Program(progName, blockNode);

            Eat(TokenType.DOT);

            return programNode;
        }

        private AST Block()
        {
            var declarationNodes = Declarations();
            var compoundStatementNode = CompoundStatement();
            var node = new Block(declarationNodes, compoundStatementNode);
            return node;
        }

        private List<AST> Declarations()
        {
            // declarations : VAR (variable_declaration SEMI)+ | empty

            var declarations = new List<AST>();

            if (currentToken.TokenType == TokenType.VAR)
            {
                Eat(TokenType.VAR);
                while (currentToken.TokenType == TokenType.ID)
                {
                    List<AST> varDecl = VariableDeclaration();
                    declarations.AddRange(varDecl);
                    Eat(TokenType.SEMI);
                }
            }

            return declarations;
        }

        private List<AST> VariableDeclaration()
        {
            // variable_declaration : ID (COMMA ID)* COLON type_spec
            var varNodes = new List<AST> { new Var(currentToken) };
            Eat(TokenType.ID);

            while (currentToken.TokenType == TokenType.COMMA)
            {
                Eat(TokenType.COMMA);
                varNodes.Add(new Var(currentToken));
                Eat(TokenType.ID);
            }

            Eat(TokenType.COLON);

            AST typeNode = TypeSpec();
            var varDeclarations = new List<AST>();
            foreach (var node in varNodes)
            {
                varDeclarations.Add(new VarDecl(node, typeNode));
            }

            return varDeclarations;
        }

        private AST TypeSpec()
        {
            // type_spec : INTEGER | REAL

            var token = currentToken;
            if (currentToken.TokenType == TokenType.INTEGER)
            {
                Eat(TokenType.INTEGER);
            }
            else
            {
                Eat(TokenType.REAL);
            }
            return new Type(token);
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

        private AST Term()
        {
            var node = Factor();

            while (currentToken.TokenType == TokenType.MUL
                || currentToken.TokenType == TokenType.INTEGER_DIV
                || currentToken.TokenType == TokenType.FLOAT_DIV)
            {
                var token = currentToken;
                if (token.TokenType == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                }
                else if (token.TokenType == TokenType.INTEGER_DIV)
                {
                    Eat(TokenType.INTEGER_DIV);
                }
                else if (token.TokenType == TokenType.FLOAT_DIV)
                {
                    Eat(TokenType.FLOAT_DIV);
                }

                node = new BinOp(left: node, op: token, right: Factor());
            }

            return node;
        }

        private AST Factor()
        {
            /* factor : PLUS factor
                  | MINUS factor
                  | INTEGER_CONST
                  | REAL_CONST
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
            else if (token.TokenType == TokenType.INTEGER_CONST)
            {
                Eat(TokenType.INTEGER_CONST);
                return new Num(token);
            }
            else if (token.TokenType == TokenType.REAL_CONST)
            {
                Eat(TokenType.REAL_CONST);
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
