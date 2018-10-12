using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class Interpreter : NodeVisitor
    {
        private readonly Parser parser;
        public readonly Dictionary<string, double> GLOBAL_VARIABLES = new Dictionary<string, double>();

        public Interpreter(Parser parser)
        {
            this.parser = parser;
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

        public override object VisitBinOp(BinOp node)
        {
            if (node.op.TokenType == TokenType.PLUS)
            {
                return (double)Visit(node.left) + (double)Visit(node.right);
            }
            if (node.op.TokenType == TokenType.MINUS)
            {
                return (double)Visit(node.left) - (double)Visit(node.right);
            }
            if (node.op.TokenType == TokenType.MUL)
            {
                return (double)Visit(node.left) * (double)Visit(node.right);
            }
            if (node.op.TokenType == TokenType.INTEGER_DIV)
            {
                return double.Parse(Visit(node.left).ToString()) / double.Parse(Visit(node.right).ToString());
            }
            if (node.op.TokenType == TokenType.FLOAT_DIV)
            {
                return (double)Visit(node.left) / (double)Visit(node.right);
            }
            throw new Exception("Unknown node!");
        }

        public override object VisitNum(Num node)
        {
            return node.value;
        }

        public override object VisitUnary(UnaryOp node)
        {
            if (node.op.TokenType == TokenType.PLUS)
            {
                return Visit(node.expr);
            }
            if (node.op.TokenType == TokenType.MINUS)
            {
                return -1 * (double)Visit(node.expr);
            }

            throw new Exception("Unknown node!");
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
            GLOBAL_VARIABLES[varName] = (double)Visit(node.Right);
        }

        public override object VisitVar(Var node)
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

        public override void VisitType(Type node)
        {
        }

        public override void VisitVarDecl(VarDecl node)
        {
        }

        public override void VisitBlock(Block node)
        {
            foreach (var decl in node.Declarations)
            {
                Visit(decl);
            }
            Visit(node.CompoundStatement);
        }

        public override void VisitProgram(Program node)
        {
            Visit(node.Block);
        }
    }
}
