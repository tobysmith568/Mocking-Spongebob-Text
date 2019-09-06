using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingSpongebobText
{
    public enum MockType
    {
        AllUpper,
        AllLower,
        AlternatingStartingUpper,
        AlternatingStartingLower,
        AlternatingWordsAlwaysStartingUpper,
        AlternatingWordsAlwaysStartingLower,
        AllRandom,
        RandomAlwaysStartingUpper,
        RandomAlwaysStartingLower,
        RandomWordsAlwaysStartingUpper,
        RandomWordsAlwaysStartingLower
    }

    public static class MockTypeUtils
    {
        private static readonly Random random = new Random();

        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public static string ReadableText(this MockType mockType)
        {
            switch (mockType)
            {
                case MockType.AllUpper:
                    return "All upper";
                case MockType.AllLower:
                    return "All lower";
                case MockType.AlternatingStartingUpper:
                    return "Alternating starting upper";
                case MockType.AlternatingStartingLower:
                    return "Alternating starting lower";
                case MockType.AlternatingWordsAlwaysStartingUpper:
                    return "Alternating words always starting upper";
                case MockType.AlternatingWordsAlwaysStartingLower:
                    return "Alternating words always starting lower";
                case MockType.AllRandom:
                    return "All random";
                case MockType.RandomAlwaysStartingUpper:
                    return "Random always starting upper";
                case MockType.RandomAlwaysStartingLower:
                    return "Random always starting lower";
                case MockType.RandomWordsAlwaysStartingUpper:
                    return "Random words always starting upper";
                case MockType.RandomWordsAlwaysStartingLower:
                    return "Random words always starting upper";
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public static string MockifyText(this MockType mockType, string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));

            switch (mockType)
            {
                case MockType.AllUpper:
                    return AllUpper(text);
                case MockType.AllLower:
                    return AllLower(text);

                case MockType.AlternatingStartingUpper:
                    return Alternating(text, true);
                case MockType.AlternatingStartingLower:
                    return Alternating(text, false);

                case MockType.AlternatingWordsAlwaysStartingUpper:
                    return AlternatingWordsAlwaysStarting(text, true);
                case MockType.AlternatingWordsAlwaysStartingLower:
                    return AlternatingWordsAlwaysStarting(text, false);

                case MockType.AllRandom:
                    return AllRandom(text);

                case MockType.RandomAlwaysStartingUpper:
                    return RandomAlwaysStartingUpper(text);
                case MockType.RandomAlwaysStartingLower:
                    return RandomAlwaysStartingLower(text);

                case MockType.RandomWordsAlwaysStartingUpper:
                    return RandomWordsAlwaysStartingUpper(text);
                case MockType.RandomWordsAlwaysStartingLower:
                    return RandomWordsAlwaysStartingLower(text);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static string AllUpper(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                result += text[i].ToUpper();
            }

            return result;
        }

        private static string AllLower(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                result += text[i].ToLower();
            }

            return result;
        }

        private static string Alternating(string text, bool startUpper)
        {
            string result = string.Empty;
            int remainder = startUpper ? 0 : 1;

            for (int i = 0; i < text.Length; i++)
            {
                result += i % 2 == remainder ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }

        private static string AlternatingWordsAlwaysStarting(string text, bool startingUpper)
        {
            string result = string.Empty;
            bool nextUpper = startingUpper;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0 || text[i - 1] == ' ')
                {
                    nextUpper = startingUpper;
                }

                result += nextUpper ? text[i].ToUpper() : text[i].ToLower();
                nextUpper = !nextUpper;
            }

            return result;
        }

        private static string AllRandom(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                result += random.NextDouble() >= 0.5 ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }

        private static string RandomAlwaysStartingUpper(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0)
                {
                    result += text[i].ToUpper();
                    continue;
                }

                result += random.NextDouble() >= 0.5 ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }

        private static string RandomAlwaysStartingLower(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0)
                {
                    result += text[i].ToLower();
                    continue;
                }

                result += random.NextDouble() >= 0.5 ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }

        private static string RandomWordsAlwaysStartingUpper(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0 || text[i - 1] == ' ')
                {
                    result += text[i].ToUpper();
                    continue;
                }

                result += random.NextDouble() >= 0.5 ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }

        private static string RandomWordsAlwaysStartingLower(string text)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0 || text[i - 1] == ' ')
                {
                    result += text[i].ToLower();
                    continue;
                }

                result += random.NextDouble() >= 0.5 ? text[i].ToUpper() : text[i].ToLower();
            }

            return result;
        }
    }

    public static class CharUtils
    {
        public static char ToUpper(this char c)
        {
            return TestRules(c) ?? char.ToUpper(c);
        }

        public static char ToLower(this char c)
        {
            return TestRules(c) ?? char.ToLower(c);
        }

        private static char? TestRules(char c)
        {
            if (MainWindow.UpperCaseLs && (c == 'l' || c == 'L'))
            {
                return 'L';
            }

            if (MainWindow.LowerCaseIs && (c == 'i' || c == 'I'))
            {
                return 'i';
            }

            if (MainWindow.LowerCaseOs && (c == 'o' || c == 'O'))
            {
                return 'o';
            }

            return null;
        }
    }
}
