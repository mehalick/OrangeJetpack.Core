using System.Text.RegularExpressions;

namespace OrangeJetpack.Core.Formatting
{
    public class StringFormatter
    {
        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        private static readonly Regex NotDigitsRegex = new Regex(@"[^(0-9|/\u0660-\u0669/)]", RegexOptions.Compiled);

        /// <summary>
        /// Gets a string with all non-numeric digits removed.
        /// </summary>
        public static string StripNonDigits(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return NotDigitsRegex.Replace(input, "").Trim();
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        private static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Gets a string with all HTML tags removed.
        /// </summary>
        public static string StripHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return HtmlRegex.Replace(input, "");
        }

        /// <summary>
        /// Compiled regular expressions for performance.
        /// </summary>
        private static readonly Regex LocalizationRegex1 = new Regex(@"^\[\{", RegexOptions.Compiled);
        private static readonly Regex LocalizationRegex2 = new Regex(@"""?}]$", RegexOptions.Compiled);
        private static readonly Regex LocalizationRegex3 = new Regex(@"""k"":""[a-z]{2}"",""v"":""", RegexOptions.Compiled);
        private static readonly Regex LocalizationRegex4 = new Regex(@"""},\{", RegexOptions.Compiled);

        /// <summary>
        /// Gets a string with localization JSON removed.
        /// </summary>
        public static string StripLocalizationJson(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = LocalizationRegex1.Replace(input, "");
            input = LocalizationRegex2.Replace(input, "");
            input = LocalizationRegex3.Replace(input, "");
            input = LocalizationRegex4.Replace(input, " ");

            return input;
        }

        /// <summary>
        /// Gets a string with MS Word HTML removed.
        /// </summary>
        public static string StripWordHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = StripHtmlComments(input);
            input = StripXmlTags(input);
            input = StripWordTags(input);
            input = StripEmptyTags(input);

            return input;
        }

        private static string StripHtmlComments(string input)
        {
            var regex = new Regex(@"\<!--.*?-->", RegexOptions.Singleline);
            return regex.Replace(input, "");
        }

        private static string StripXmlTags(string input)
        {
            var regex = new Regex(@"<xml>.*?</xml>", RegexOptions.Singleline);
            return regex.Replace(input, "");
        }

        private static string StripWordTags(string input)
        {
            var regex = new Regex(@"<([a-z]{1}:\w+)>.*?</(\1)>", RegexOptions.Singleline);
            return regex.Replace(input, "");
        }

        private static string StripEmptyTags(string input)
        {
            var regex = new Regex(@"<(\w+)></(\1)>", RegexOptions.Singleline);
            return regex.Replace(input, "");
        }
    }
}
