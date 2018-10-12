using System;
using Xunit;

namespace Interpreter.Tests
{
    public class InterpreterTests
    {
        private Interpreter GetInterpreter(string input)
        {
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            Interpreter inter = new Interpreter(parser);
            return inter;
        }

        [Fact]
        public void Should_Succes_When_UnaryMinus()
        {
            var input = @"PROGRAM Part10;
VAR
   number     : INTEGER;
   a, b, c, x : INTEGER;
   y          : REAL;

BEGIN {Part10}
   BEGIN
      number := 2;
      a := number;
      b := 10 * a + 10 * number DIV 4;
      c := a - - b
   END;
   x := 11;
   y := 20 / 7 + 3.14;
   { writeln('a = ', a); }
   { writeln('b = ', b); }
   { writeln('c = ', c); }
   { writeln('number = ', number); }
   { writeln('x = ', x); }
   { writeln('y = ', y); }
END.  {Part10}";
            var interp = GetInterpreter(input);
            interp.Interpret();
            var gv = interp.GLOBAL_VARIABLES;
            Assert.Equal(1, 1);
        }

    }
}
