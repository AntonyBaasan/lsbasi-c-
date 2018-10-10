using System;

namespace Interpreter
{
    public class Interpreter : NodeVisitor
    {
        private readonly Parser parser;

        public Interpreter(Parser parser)
        {
            this.parser = parser;
        }

        public override int VisitBinOp(BinOp node)
        {
            if (node.op.TokenType == TokenType.PLUS)
            {
                return Visit(node.left) + Visit(node.right);
            }
            if (node.op.TokenType == TokenType.MINUS)
            {
                return Visit(node.left) - Visit(node.right);
            }
            if (node.op.TokenType == TokenType.MUL)
            {
                return Visit(node.left) * Visit(node.right);
            }
            if (node.op.TokenType == TokenType.DIV)
            {
                return Visit(node.left) / Visit(node.right);
            }

            throw new Exception("Unknown node!");
        }

        public override int VisitNum(Num node)
        {
            return node.value;
        }

        public int Interpret()
        {
            var tree = parser.Parse();
            return Visit(tree);
        }

    }
}
