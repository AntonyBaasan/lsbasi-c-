using System;
using Xunit;

namespace Interpreter.Tests
{
    public class InterpreterTests
    {
        [Fact]
        public void ShouldReturn_Correct_ExpressionResult()
        {
            string text = "3+2";
            Interpreter inter = new Interpreter(text);
            Assert.Equal(5, inter.Expr());
        }

        [Theory]
        [InlineData("3+")]
        [InlineData("+")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("3 2")]
        public void ShouldThrowException1(string text)
        {
            Interpreter inter = new Interpreter(text);
            Assert.Throws<Exception>(() => inter.Expr());
        }
    }
}
