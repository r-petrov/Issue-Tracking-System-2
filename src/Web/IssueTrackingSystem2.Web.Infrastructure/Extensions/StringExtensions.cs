namespace IssueTrackingSystem2.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string SplitStringByCapitalLetters(this string str)
        {
            var r = new Regex(
                @"(?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return string.Join(" ", Regex.Split(str, @"(?<!^)(?=[A-Z](?![A-Z]|$))"));
        }
    }
}
