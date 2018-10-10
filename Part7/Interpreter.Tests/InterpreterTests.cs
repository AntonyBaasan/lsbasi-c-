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
        [InlineData("7 + 3", "73+")]
        [InlineData("7 + 4 - 3", "74+3-")]
        [InlineData("7 + (4 - 3)", "743-+")]
        [InlineData("(5 + 3) * 12 / 3", "53+12*3/")]
        [InlineData("2 + 3 * 5", "235*+")]
        public void Should_Succes_When_PolishNotation(string input, string result)
        {
            Assert.Equal(result, GetInterpreter(input).GetPolishNotation());
        }

        [Theory]
        [InlineData("7 + 3 * (10 / (12 / (3 + 1) - 1)))", 22)]
        [InlineData("7 + 3 * (10 / (12 / (3 + 1) - 1)) / (2 + 3) - 5 - 3 + (8)", 10)]
        [InlineData("7 + (((3 + 2)))", 12)]
        public void Should_Succes_When_Parentheses(string input, int result)
        {
            Assert.Equal(result, GetInterpreter(input).Interpret());
        }

        [Theory]
        [InlineData("3", 3)]
        [InlineData("2 + 7 * 4", 30)]
        [InlineData("7 - 8 / 4", 5)]
        [InlineData("14 + 2 * 3 - 6 / 2", 17)]
        public void Should_Succes_When_MultipAndDivAndAddAndMinus(string input, int result)
        {
            Assert.Equal(result, GetInterpreter(input).Interpret());
        }

        [Theory]
        [InlineData("2*9/3", 6)]
        [InlineData("4 * 2", 8)]
        [InlineData("10/5", 2)]
        [InlineData("14 + 2 * 3 - 6 / 2", 17)]
        public void Should_Succes_When_MultipAndDivMultipleNumbers(string input, int result)
        {
            Assert.Equal(result, GetInterpreter(input).Interpret());
        }

        [Fact]
        public void Should_Succes_When_AddAndSubMultipleNumbers()
        {
            string text = "3+2-1+9";
            Assert.Equal(13, GetInterpreter(text).Interpret());
        }

        [Fact]
        public void Should_Succes_When_Add()
        {
            string text = "3+2";
            Assert.Equal(5, GetInterpreter(text).Interpret());
        }

        [Fact]
        public void Should_Succes_When_Substract()
        {
            string text = "3-2";
            Assert.Equal(1, GetInterpreter(text).Interpret());
        }

        [Fact]
        public void Should_Succes_When_SubstractMultipDigit()
        {
            string text = "13-11";
            Assert.Equal(2, GetInterpreter(text).Interpret());
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
            Interpreter inter = GetInterpreter(text);
            Assert.ThrowsAny<Exception>(() => inter.Interpret());
        }
    }
}
