using RomanNumerals;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsTests
{
    public class RomanNumeralsManagerTests
    {
        [Theory]
        [InlineData(1999, "MCMXCIX")]
        [InlineData(2444, "MMCDXLIV")]
        [InlineData(90, "XC")]
        public void GetRomanNumeral_returns_correct_roman_numeral(int value, string romanNumeral)
        {
            var sut = new RomanNumeralsManager();
            sut.GetRomanNumeral(value).ShouldBe(romanNumeral);
        }

        [Theory]
        [InlineData(1999, "MCMXCIX")]
        [InlineData(2444, "MMCDXLIV")]
        [InlineData(90, "XC")]
        public void GetValue_returns_correct_value(int value, string romanNumeral)
        {
            var sut = new RomanNumeralsManager();
            sut.GetValue(romanNumeral).ShouldBe(value);
        }

        [Theory]
        [InlineData("MQ")]
        [InlineData("IM")]
        [InlineData("IIV")]
        [InlineData("IIII")]
        public void GetValue_throws_when_invalid_roman_numeral(string romanNumeral)
        {
            var sut = new RomanNumeralsManager();
            Should.Throw<ApplicationException>(() => sut.GetValue(romanNumeral));
        }
    }
}
