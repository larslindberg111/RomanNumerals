namespace RomanNumerals
{
    public class RomanNumeralsManager
    {
        public string GetRomanNumeral(int value)
        {
            var tokens = ValueConverter.GetTokenList(value);
            return RomanNumeralConverter.GetRomanNumeral(tokens);
        }

        public int GetValue(string romanNumeral)
        {
            var tokens = RomanNumeralConverter.GetTokenList(romanNumeral);
            return ValueConverter.GetValue(tokens);
        }
    }
}
