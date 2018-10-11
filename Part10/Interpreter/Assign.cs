namespace Interpreter
{
    public class Assign: AST
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
}
