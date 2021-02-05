namespace ImageCropDataTypeGenerator.Umbraco.Extensions
{
    internal static class ObjectExtensions
    {
        public static bool HasValue(this object value)
            => !(value == null || value.Equals(default));
    }
}