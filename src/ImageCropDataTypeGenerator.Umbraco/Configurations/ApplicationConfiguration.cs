using ImageCropDataTypeGenerator.Umbraco.Helpers;

namespace ImageCropDataTypeGenerator.Umbraco.Configurations
{
    internal static class ApplicationConfiguration
    {
        public static readonly string GeneratedClassNamespace = AppSettings.Get<string>("ImageCropDataTypeGenerator.Namespace");
    }
}