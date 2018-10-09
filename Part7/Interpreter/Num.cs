using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class Num: AST
    {
        private readonly Token token;
        private readonly string value;

        public Num(Token token)
        {
            this.token = token;
            this.value = token.Value;
        }
    }
}
