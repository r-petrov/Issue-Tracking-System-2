namespace IssueTrackingSystem2.Common.Infrastructure.Extensions
{
    using System;
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

        public static string ApendStringCapitalLetters(this string str)
        {
            var projectNameParts = str.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            var result = new System.Text.StringBuilder();
            foreach (var projectNamePart in projectNameParts)
            {
                result.Append(projectNamePart[0]);
            }

            return result.ToString();
        }
    }
}
