using System;
using Xunit;

namespace Interpreter.Tests
{
    public class InterpreterTests
    {
        [Fact]
        public void Should_Succes_When_MultipAndDivAndAddAndMinus()
        {
            string text = "2*9/3+2";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(8, inter.Expr());
        }

        [Fact]
        public void Should_Succes_When_MultipAndDivMultipleNumbers()
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

        [Fact]
        public void Should_Succes_When_AddAndSubMultipleNumbers()
        {
            string text = "3+2-1+9";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(13, inter.Expr());
        }

        [Fact]
        public void Should_Succes_When_Add()
        {
            string text = "3+2";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(5, inter.Expr());
        }


        [Fact]
        public void Should_Succes_When_Substract()
        {
            string text = "3-2";
            var lexer = new Lexer(text);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(1, inter.Expr());
        }

        [Fact]
        public void Should_Succes_When_SubstractMultipDigit()
        {
            string text = "13-11";
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
