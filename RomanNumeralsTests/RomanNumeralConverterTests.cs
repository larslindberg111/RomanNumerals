using RomanNumerals;
using Shouldly;

namespace RomanNumeralsTests
{
    public class RomanNumeralConverterTests
    {
        [Fact]
        public void GetTokenList_returns_correct_tokenList()
        {
            var expected = new List<Token> { Token.M, Token.D, Token.C, Token.L, Token.X, Token.V, Token.I };
            RomanNumeralConverter.GetTokenList("MDCLXVI").ShouldBe(expected);
        }

        [Fact]
        public void GetTokenList_throws_when_invalid_token()
        {
            Should.Throw<Exception>(() => RomanNumeralConverter.GetTokenList("IQ"));
        }

        [Fact]
        public void GetRomanNumeral_returns_correct_roman_numeral()
        {
            var tokenList = new List<Token> { Token.M, Token.D, Token.C, Token.L, Token.X, Token.V, Token.I };
            RomanNumeralConverter.GetRomanNumeral(tokenList).ShouldBe("MDCLXVI");
        }
    }
}
