using System.Collections.Generic;

namespace Interpreter
{
    public class Compound: AST
    {
        public List<AST> Children { get; }

        public Compound()
        {
            Children = new List<AST>();
        }

    }
}
