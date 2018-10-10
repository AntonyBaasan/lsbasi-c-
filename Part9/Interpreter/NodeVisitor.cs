
using System;

namespace Interpreter
{
    public abstract class NodeVisitor
    {
        public int Visit(AST node)
        {
            if (node.GetType() == typeof(BinOp))
            {
                return VisitBinOp((BinOp)node);
            }
            else if (node.GetType() == typeof(Num))
            {
                return VisitNum((Num)node);
            }
            else if (node.GetType() == typeof(UnaryOp))
            {
                return VisitUnary((UnaryOp)node);
            }

            throw new Exception("Unknown node!");
        }

        // Visitor that makes Polish Natation
        public string VisitForPN(AST node) {
            string result = "";

            if (node.GetType() == typeof(BinOp))
            {
                string leftResult = VisitForPN(((BinOp)node).left);
                string rightResult = VisitForPN(((BinOp)node).right);
                result = leftResult + rightResult + ((BinOp)node).op.Value;
            }
            else if (node.GetType() == typeof(Num))
            {
                result = ((Num)node).value.ToString();
            }
            
            return result;
        }

        public abstract int VisitBinOp(BinOp op);

        public abstract int VisitNum(Num num);

        public abstract int VisitUnary(UnaryOp op);
    }
}
