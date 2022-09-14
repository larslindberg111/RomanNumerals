namespace RomanNumerals
{
    public static class RomanNumeralConverter
    {
        public static List<Token> GetTokenList(string romanNumeral)
        {
            return romanNumeral
                .Select(x => GetToken(x))
                .ToList();
        }

        private static Token GetToken(char c)
        {
            switch(c)
            {
                case 'I':
                    return Token.I;
                case 'V':
                    return Token.V;
                case 'X':
                    return Token.X;
                case 'L':
                    return Token.L;
                case 'C':
                    return Token.C;
                case 'D':
                    return Token.D;
                case 'M':
                    return Token.M;
                default:
                    throw new ApplicationException($"Unknown character: {c}");
            }
        }

        public static string GetRomanNumeral(List<Token> tokens)
        {
            return string.Join("", tokens.Select(x => GetChar(x)));
        }

        private static string GetChar(Token token)
        {
            switch (token)
            {
                case Token.I:
                    return "I";
                case Token.V:
                    return "V";
                case Token.X:
                    return "X";
                case Token.L:
                    return "L";
                case Token.C:
                    return "C";
                case Token.D:
                    return "D";
                case Token.M:
                    return "M";
                default:
                    throw new ApplicationException($"Unknown symbol {token}");
            }
        }

    }
}
