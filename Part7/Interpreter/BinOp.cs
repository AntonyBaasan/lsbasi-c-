namespace Interpreter
{
    public class BinOp: AST
    {
        private readonly Token token;
        private readonly AST left;
        private readonly Token op;
        private readonly AST right;

        public BinOp(AST left, Token op, AST right)
        {
            this.left = left;
            this.op = op;
            this.token = op;
            this.right = right;
        }
    }
}
