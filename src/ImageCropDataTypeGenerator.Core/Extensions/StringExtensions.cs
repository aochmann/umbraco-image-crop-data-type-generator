using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ImageCropDataTypeGenerator.Core.Extensions
{
    internal static class StringExtensions
    {
        public static string ToPascalCase(this string original)
        {
            var invalidCharsRgx = new Regex("[^-_a-zA-Z0-9]");
            var whiteSpace = new Regex(@"(?<=\s)");
            var startsWithLowerCaseChar = new Regex("^[a-z]");
            var firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            var lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            var upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            var pascalCase = invalidCharsRgx
                .Replace(whiteSpace.Replace(original, "_"), string.Empty)
                .Split(new[] { "_", "-" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }

        public static bool HasValue(this string input)
            => !string.IsNullOrWhiteSpace(input);
    }
}