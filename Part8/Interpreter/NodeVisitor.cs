
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

            return -1;
        }

        public abstract int VisitBinOp(BinOp op);

        public abstract int VisitNum(Num num);
    }
}
