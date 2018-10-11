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

        [Theory]
        [InlineData(2, "number")]
        [InlineData(2, "a")]
        [InlineData(25, "b")]
        [InlineData(27, "c")]
        [InlineData(11, "x")]
        public void Should_Succes_When_UnaryMinus(int value, string varName)
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
            Assert.Equal(value, gv[varName]);
        }

    }
}
