using System;
using Xunit;

namespace Interpreter.Tests
{
    public class InterpreterTests
    {
        [Fact]
        public void Should_Succes_When_AddAndSubMultipleNumbers()
        {
            string text = "2*9/3";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(6, inter.Expr());
        }

        [Fact]
        public void Should_Succes_When_Multiply()
        {
            string text = "4*2";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(8, inter.Expr());
        }


        [Fact]
        public void Should_Succes_When_Division()
        {
            string text = "10/5";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(2, inter.Expr());
        }

        [Theory]
        [InlineData("")]
        public void Should_ConstructorThrowException_WhenInvalidExpression(string text)
        {
            Assert.ThrowsAny<Exception>(() => new Lexer(text));
        }

        [Theory]
        [InlineData("3*")]
        [InlineData("*")]
        public void Should_ThrowException_WhenInvalidExpression(string text)
        {
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.ThrowsAny<Exception>(() => inter.Expr());
        }
    }
}
