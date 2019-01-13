using System.Text.RegularExpressions;

namespace WordCountListing
{
    internal static class RegexRepository
    {
        /// <summary>
        /// Matches any sequence of 1 or more characters until a whitespace, tabulator or newline character is encountered
        /// </summary>
        /// <remarks>Matches for example: "!", "word," hy-phen", "612"</remarks>
        public static readonly Regex PrintableSequenceMatcher = new Regex(@"\S+");

        /// <summary>
        /// Matches any sequence of 1 or more 'word characters' (A-Z,a-z,0-9,_)
        /// </summary>
        /// <remarks>Example: text "To be, or not_to_be, th4t is TeH 1 ques-tion!" 
        /// yields following matches: "To", "be", "or", "not_to_be", "th4t" "is" "TeH" "1" "ques", "tion".</remarks>
        private static readonly Regex RealWordMatcher = new Regex(@"\w+");
    }
}