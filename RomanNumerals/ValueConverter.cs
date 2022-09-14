namespace RomanNumerals
{
    public static class ValueConverter
    {
        public static int GetValue(List<Token> tokens)
        {
            if(!IsValid(tokens))
            {
                throw new ApplicationException($"Invalid roman numeral");
            }
            var total = 0;
            for (int i = 0; i < tokens.Count; i++)
            {
                var tokenValue = GetTokenValue(tokens[i]);
                if (i < tokens.Count - 1)
                {
                    var nextTokenValue = GetTokenValue(tokens[i + 1]);
                    if (nextTokenValue > tokenValue)
                    {
                        tokenValue = -tokenValue;
                    }
                }
                total += tokenValue;
            }
            return total;
        }

        private static int GetTokenValue(Token token)
        {
            switch (token)
            {
                case Token.I:
                    return 1;
                case Token.V:
                    return 5;
                case Token.X:
                    return 10;
                case Token.L:
                    return 50;
                case Token.C:
                    return 100;
                case Token.D:
                    return 500;
                case Token.M:
                    return 1000;
                default:
                    throw new ApplicationException("Unknown token");
            }
        }

        public static List<Token> GetTokenList(int number)
        {
            var result = new List<Token>();
            TakePart(ref number, result, 1000, new List<Token> { Token.M });
            TakePart(ref number, result, 900, new List<Token> { Token.C, Token.M });
            TakePart(ref number, result, 500, new List<Token> { Token.D });
            TakePart(ref number, result, 400, new List<Token> { Token.C, Token.D });
            TakePart(ref number, result, 100, new List<Token> { Token.C });
            TakePart(ref number, result, 90, new List<Token> { Token.X, Token.C });
            TakePart(ref number, result, 50, new List<Token> { Token.L });
            TakePart(ref number, result, 40, new List<Token> { Token.X, Token.L });
            TakePart(ref number, result, 10, new List<Token> { Token.X });
            TakePart(ref number, result, 9, new List<Token> { Token.I, Token.X });
            TakePart(ref number, result, 5, new List<Token> { Token.V });
            TakePart(ref number, result, 4, new List<Token> { Token.I, Token.V });
            TakePart(ref number, result, 1, new List<Token> { Token.I });
            return result;
        }

        private static void TakePart(ref int count, List<Token> tokens, int partLimit, List<Token> partTokens)
        {
            while (count >= partLimit)
            {
                tokens.AddRange(partTokens);
                count -= partLimit;
            }
        }

        public static bool IsValid(List<Token> tokens)
        {
            var result = !IsEmpty(tokens)
                && !IsTokenPrecededByMoreThanOneSmallerTokens(tokens)
                && !IsTokenFollowedByLargerTokenOfNonMatchingType(tokens)
                && !IsTokenSameAsTheThreeFollowingTokens(tokens);
            return result;
        }

        private static bool IsEmpty(List<Token> tokens)
        {
            return tokens.Count == 0;
        }

        private static bool IsTokenPrecededByMoreThanOneSmallerTokens(List<Token> tokens)
        {
            for (int i = 2; i < tokens.Count; i++)
            {
                var tokenValue = GetTokenValue(tokens[i]);
                var oneBeforeTokenValue = GetTokenValue(tokens[i - 1]);
                var twoBeforeTokenValue = GetTokenValue(tokens[i - 2]);
                if (oneBeforeTokenValue < tokenValue && twoBeforeTokenValue < tokenValue)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsTokenFollowedByLargerTokenOfNonMatchingType(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                var firstToken = tokens[i];
                var secondToken = tokens[i + 1];
                if (GetTokenValue(firstToken) >= GetTokenValue(secondToken))
                {
                    continue;
                }
                if (firstToken == Token.I)
                {
                    var allowedFollowers = new List<Token>() { Token.V, Token.X };
                    if (!allowedFollowers.Contains(secondToken))
                    {
                        return true;
                    }
                }
                if (firstToken == Token.X)
                {
                    var allowedFollowers = new List<Token>() { Token.L, Token.C };
                    if (!allowedFollowers.Contains(secondToken))
                    {
                        return true;
                    }
                }
                if (firstToken == Token.C)
                {
                    var allowedFollowers = new List<Token>() { Token.D, Token.M };
                    if (!allowedFollowers.Contains(secondToken))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsTokenSameAsTheThreeFollowingTokens(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count - 3; i++)
            {
                var firstToken = tokens[i];
                var secondToken = tokens[i + 1];
                var thirdToken = tokens[i + 2];
                var fourthToken = tokens[i + 3];
                if (firstToken == secondToken && firstToken == thirdToken && firstToken == fourthToken)
                {
                    return true;
                }
            }
            return false;
        } 
    }
}