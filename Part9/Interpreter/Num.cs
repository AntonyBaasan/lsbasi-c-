namespace Interpreter
{
    public class Num: AST
    {
        private readonly Token token;
        public readonly int value;

        public Num(Token token)
        {
            this.token = token;
            this.value = int.Parse(token.Value);
        }
    }
}
