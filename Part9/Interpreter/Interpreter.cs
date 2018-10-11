using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class Interpreter : NodeVisitor
    {
        private readonly Parser parser;
        public readonly Dictionary<string, int> GLOBAL_VARIABLES = new Dictionary<string, int>();

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

        public override int VisitUnary(UnaryOp node)
        {
            if (node.op.TokenType == TokenType.PLUS)
            {
                return Visit(node.expr);
            }
            if (node.op.TokenType == TokenType.MINUS)
            {
                return -1 * Visit(node.expr);
            }

            throw new Exception("Unknown node!");
        }

        public void Interpret()
        {
            var tree = parser.Parse();
            if (tree != null)
            {
                Visit(tree);
            }
        }

        public string GetPolishNotation()
        {
            var tree = parser.Parse();
            return VisitForPN(tree);
        }

        public override void VisitCompound(Compound node)
        {
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        public override void VisitAssign(Assign node)
        {
            var varName = node.Left.Value;
            GLOBAL_VARIABLES[varName] = Visit(node.Right);
        }

        public override int VisitVar(Var node)
        {
            var varName = node.Value;
            if (GLOBAL_VARIABLES.ContainsKey(varName))
            {
                return GLOBAL_VARIABLES[varName];
            }

            throw new Exception($"Variable {varName} doesn't exist!");
        }

        public override void VisitNoOp(NoOp node)
        {
        }
    }
}
