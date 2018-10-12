
using System;

namespace Interpreter
{
    public abstract class NodeVisitor
    {
        public object Visit(AST node)
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
            else if (node.GetType() == typeof(Compound))
            {
                VisitCompound((Compound)node);
                return null;
            }
            else if (node.GetType() == typeof(Assign))
            {
                VisitAssign((Assign)node);
                return null;
            }
            else if (node.GetType() == typeof(Var))
            {
                return VisitVar((Var)node);
            }
            else if (node.GetType() == typeof(NoOp))
            {
                VisitNoOp((NoOp)node);
                return null;
            }
            else if (node.GetType() == typeof(Program))
            {
                VisitProgram((Program)node);
                return null;
            }
            else if (node.GetType() == typeof(Block))
            {
                VisitBlock((Block)node);
                return null;
            }
            else if (node.GetType() == typeof(VarDecl))
            {
                VisitVarDecl((VarDecl)node);
                return null;
            }
            else if (node.GetType() == typeof(Type))
            {
                VisitType((Type)node);
                return null;
            }

            throw new Exception("Unknown node!");
        }

        // Visitor that makes Polish Natation
        public string VisitForPN(AST node)
        {
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

        public abstract object VisitBinOp(BinOp op);
        public abstract object VisitNum(Num num);
        public abstract object VisitUnary(UnaryOp op);
        public abstract void VisitCompound(Compound node);
        public abstract void VisitAssign(Assign node);
        public abstract object VisitVar(Var node);
        public abstract void VisitNoOp(NoOp node);
        public abstract void VisitType(Type node);
        public abstract void VisitVarDecl(VarDecl node);
        public abstract void VisitBlock(Block node);
        public abstract void VisitProgram(Program node);

    }
}
