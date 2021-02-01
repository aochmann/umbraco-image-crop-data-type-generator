namespace ImageCropDataTypeGenerator.Umbraco.Extensions
{
    internal static class StringExtensions
    {
        public static bool HasValue(this string input)
            => !string.IsNullOrWhiteSpace(input);
    }
}