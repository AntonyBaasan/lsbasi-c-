namespace Interpreter
{
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
}
