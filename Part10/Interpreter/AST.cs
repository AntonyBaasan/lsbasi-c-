using System.Collections.Generic;

namespace Interpreter
{
    // Abstract-Syntax-Tree (AST)
    public class AST
    {
    }

    public class BinOp : AST
    {
        public readonly Token token;
        public readonly AST left;
        public readonly Token op;
        public readonly AST right;

        public BinOp(AST left, Token op, AST right)
        {
            this.left = left;
            this.op = op;
            this.token = op;
            this.right = right;
        }
    }

    public class Num : AST
    {
        private readonly Token token;
        public readonly int value;

        public Num(Token token)
        {
            this.token = token;
            this.value = int.Parse(token.Value);
        }
    }

    public class UnaryOp : AST
    {
        public readonly Token token;
        public readonly Token op;
        public readonly AST expr;

        public UnaryOp(Token op, AST expr)
        {
            token = op;
            this.op = op;
            this.expr = expr;
        }
    }

    public class Compound : AST
    {
        public List<AST> Children { get; }

        public Compound()
        {
            Children = new List<AST>();
        }
    }

    public class Assign : AST
    {
        public Var Left { get; }
        public Token Op { get; }
        public AST Right { get; }

        public Assign(Var left, Token op, AST right)
        {
            Left = left;
            Op = op;
            Right = right;
        }
    }

    public class Var : AST
    {
        public Token Token { get; }
        public string Value { get; }

        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }
    }

    public class NoOp : AST
    {
    }

    public class Program : AST
    {
        public string Name { get; }
        public AST Block { get; }

        public Program(string name, AST block)
        {
            Name = name;
            Block = block;
        }
    }

    public class Block : AST
    {
        public AST Declarations { get; }
        public AST CompoundStatement { get; }

        public Block(AST declarations, AST compoundStatement)
        {
            Declarations = declarations;
            CompoundStatement = compoundStatement;
        }
    }

    public class VarDecl : AST
    {
        public AST VarNode { get; }
        public AST TypeNode { get; }

        public VarDecl(AST varNode, AST typeNode)
        {
            VarNode = varNode;
            TypeNode = typeNode;
        }
    }

    public class Type : AST
    {
        public Token Token { get; }

        public Type(Token token)
        {
            Token = token;
        }
    }

}
