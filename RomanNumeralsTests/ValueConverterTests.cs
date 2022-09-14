using RomanNumerals;
using Shouldly;

namespace RomanNumeralsTests
{
    public class ValueConverterTests
    {
        [Fact]
        public void GetValue_should_get_value_for_single_token()
        {
            var singleTokenList = new List<Token> { Token.I };
            ValueConverter.GetValue(singleTokenList).ShouldBe(1);
        }

        [Fact]
        public void GetValue_should_add_values_for_repeated_tokens()
        {
            var repeatedTokenList = new List<Token> { Token.I, Token.I, Token.I };
            ValueConverter.GetValue(repeatedTokenList).ShouldBe(3);
        }

        [Fact]
        public void GetValue_should_add_value_for_token_when_it_is_followed_by_smaller_token_value()
        {
            var smallToken = Token.I;
            var largeToken = Token.V;
            var tokenList = new List<Token> { largeToken, smallToken };
            ValueConverter.GetValue(tokenList).ShouldBe(6);
        }

        [Fact]
        public void GetValue_should_substract_value_for_token_when_it_is_followed_by_larger_token_value()
        {
            var smallToken = Token.I;
            var largeToken = Token.V;
            var tokenList = new List<Token> { smallToken, largeToken };
            ValueConverter.GetValue(tokenList).ShouldBe(4);
        }

        [Theory]
        [InlineData("I", 1)]
        [InlineData("II", 2)]
        [InlineData("III", 3)]
        [InlineData("IV", 4)]
        [InlineData("V", 5)]
        [InlineData("VI", 6)]
        [InlineData("VII", 7)]
        [InlineData("VIII", 8)]
        [InlineData("IX", 9)]
        [InlineData("X", 10)]
        [InlineData("XX", 20)]
        [InlineData("XXX", 30)]
        [InlineData("XL", 40)]
        [InlineData("L", 50)]
        [InlineData("LX", 60)]
        [InlineData("LXX", 70)]
        [InlineData("LXXX", 80)]
        [InlineData("XC", 90)]
        [InlineData("C", 100)]
        [InlineData("CC", 200)]
        [InlineData("CCC", 300)]
        [InlineData("CD", 400)]
        [InlineData("D", 500)]
        [InlineData("DC", 600)]
        [InlineData("DCC", 700)]
        [InlineData("DCCC", 800)]
        [InlineData("CM", 900)]
        [InlineData("M", 1000)]
        [InlineData("MM", 2000)]
        public void GetValue_should_return_correct_value_for_each_decimal_part(string romanNumeral, int value)
        {
            var tokenlist = RomanNumeralConverter.GetTokenList(romanNumeral);
            ValueConverter.GetValue(tokenlist).ShouldBe(value);
        }

        [Theory]
        [InlineData("MCMXCIX", 1999)]
        [InlineData("MMCDXLIV", 2444)]
        [InlineData("XC", 90)]
        public void GetValue_should_return_correct_value(string romanNumeral, int value)
        {
            var tokenlist = RomanNumeralConverter.GetTokenList(romanNumeral);
            ValueConverter.GetValue(tokenlist).ShouldBe(value);
        }

        [Fact]
        public void GetValue_throws_when_invalid_token_list()
        {
            var invalidTokenList = new List<Token> { Token.I, Token.I, Token.V };
            Should.Throw<ApplicationException>(() => ValueConverter.GetValue(invalidTokenList));
        }

        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(6, "VI")]
        [InlineData(7, "VII")]
        [InlineData(8, "VIII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(20, "XX")]
        [InlineData(30, "XXX")]
        [InlineData(40, "XL")]
        [InlineData(50, "L")]
        [InlineData(60, "LX")]
        [InlineData(70, "LXX")]
        [InlineData(80, "LXXX")]
        [InlineData(90, "XC")]
        [InlineData(100, "C")]
        [InlineData(200, "CC")]
        [InlineData(300, "CCC")]
        [InlineData(400, "CD")]
        [InlineData(500, "D")]
        [InlineData(600, "DC")]
        [InlineData(700, "DCC")]
        [InlineData(800, "DCCC")]
        [InlineData(900, "CM")]
        [InlineData(1000, "M")]
        [InlineData(2000, "MM")]
        public void GetTokenList_should_return_correct_token_list_for_each_part(int value, string romanNumeral)
        {
            var tokenList = RomanNumeralConverter.GetTokenList(romanNumeral);
            ValueConverter.GetTokenList(value).ShouldBe(tokenList);
        }

        [Fact]
        public void GetTokenList_should_return_correct_token_list_for_all_parts()
        {
            var expected = new List<Token> { Token.M, Token.M, Token.C, Token.M, Token.X, Token.C, Token.I, Token.X };
            ValueConverter.GetTokenList(2999).ShouldBe(expected);
        }

        [Fact]
        public void IsValid_should_be_false_when_tokenlist_is_empty()
        {
            var tokenList = new List<Token>();
            ValueConverter.IsValid(tokenList).ShouldBeFalse();
        }

        [Fact]
        public void IsValid_should_be_true_when_larger_token_is_followed_by_smaller_tokens()
        {
            var smallToken = Token.I;
            var largeToken = Token.V;
            var tokenList = new List<Token>() { largeToken, smallToken, smallToken };
            ValueConverter.IsValid(tokenList).ShouldBeTrue();
        }

        [Fact]
        public void IsValid_should_be_true_when_token_is_repeated_three_times()
        {
            var tokenList = new List<Token>() { Token.I, Token.I, Token.I };
            ValueConverter.IsValid(tokenList).ShouldBeTrue();
        }

        [Fact]
        public void IsValid_should_be_false_when_token_is_repeated_four_times()
        {
            var tokenList = new List<Token>() { Token.I, Token.I, Token.I, Token.I };
            ValueConverter.IsValid(tokenList).ShouldBeFalse();
        }

        [Fact]
        public void IsValid_should_be_true_when_one_smaller_token_followed_by_a_larger_token()
        {
            var smallToken = Token.I;
            var largeToken = Token.V;
            var tokenList = new List<Token>() { smallToken, largeToken, largeToken };
            ValueConverter.IsValid(tokenList).ShouldBeTrue();
        }

        [Fact]
        public void IsValid_should_be_false_when_two_smaller_tokens_are_followed_by_a_larger_token()
        {
            var smallToken = Token.I;
            var largeToken = Token.V;
            var tokenList = new List<Token>() { smallToken, smallToken, largeToken };
            ValueConverter.IsValid(tokenList).ShouldBeFalse();
        }

        [Theory]
        [InlineData(Token.I, Token.V, true)]
        [InlineData(Token.I, Token.X, true)]
        [InlineData(Token.I, Token.L, false)]
        [InlineData(Token.I, Token.C, false)]
        [InlineData(Token.I, Token.D, false)]
        [InlineData(Token.I, Token.M, false)]
        [InlineData(Token.X, Token.L, true)]
        [InlineData(Token.X, Token.C, true)]
        [InlineData(Token.X, Token.D, false)]
        [InlineData(Token.X, Token.M, false)]
        [InlineData(Token.C, Token.D, true)]
        [InlineData(Token.C, Token.M, true)]
        public void IsValid_should_be_true_only_when_smaller_token_is_followed_by_matching_larger_token(Token firstToken, Token lastToken, bool expectedValid)
        {
            var tokenList = new List<Token>() { firstToken, lastToken };
            ValueConverter.IsValid(tokenList).ShouldBe(expectedValid);
        }
    }
}
