namespace Interpreter
{
    public class Var: AST
    {
        public Token Token { get; }
        public string Value { get; }

        public Var(Token token)
        {
            Token = token;
            Value = token.Value;
        }
    }
}
