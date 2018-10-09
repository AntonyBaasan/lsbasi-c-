using System;
using Xunit;

namespace Interpreter.Tests
{
    public class InterpreterTests
    {
        [Theory]
        [InlineData("7 + 3 * (10 / (12 / (3 + 1) - 1)))", 22)]
        [InlineData("7 + 3 * (10 / (12 / (3 + 1) - 1)) / (2 + 3) - 5 - 3 + (8)", 10)]
        [InlineData("7 + (((3 + 2)))", 12)]
        public void Should_Succes_When_Parentheses(string input, int result)
        {
            var lexer = new Lexer(input);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(result, inter.Expr());
        }

        [Theory]
        [InlineData("3", 3)]
        [InlineData("2 + 7 * 4", 30)]
        [InlineData("7 - 8 / 4", 5)]
        [InlineData("14 + 2 * 3 - 6 / 2", 17)]
        public void Should_Succes_When_MultipAndDivAndAddAndMinus(string input, int result)
        {
            var lexer = new Lexer(input);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(result, inter.Expr());
        }

        [Theory]
        [InlineData("2*9/3", 6)]
        [InlineData("4 * 2", 8)]
        [InlineData("10/5", 2)]
        [InlineData("14 + 2 * 3 - 6 / 2", 17)]
        public void Should_Succes_When_MultipAndDivMultipleNumbers(string input, int result)
        {
            var lexer = new Lexer(input);
            Interpreter inter = new Interpreter(lexer);
            Assert.Equal(result, inter.Expr());
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
