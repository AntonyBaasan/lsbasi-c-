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
            var input = @"BEGIN
    BEGIN
        number := 2;
        a:= number;
        b:= 10 * a + 10 * number / 4;
        c:= a - -b
    END;
        x:= 11;
            END.";
            var interp = GetInterpreter(input);
            interp.Interpret();
            var gv = interp.GLOBAL_VARIABLES;
            Assert.Equal(1, 1);
        }

    }
}
