namespace Interpreter
{
    public class BinOp: AST
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
}
