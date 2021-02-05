namespace ImageCropDataTypeGenerator.Umbraco.Options
{
    internal class Options
    {
        internal static class Defaults
        {
            public const bool Enable = false;
            public const bool AcceptUnsafeModelsDirectory = false;
            public const string Namespace = "ImageCropDataTypeGenerator.Models";
            public const string ModelsDirectory = "~/App_Data/Models";
        }

        public bool Enable { get; set; } = Defaults.Enable;
        public bool AcceptUnsafeModelsDirectory { get; set; } = Defaults.AcceptUnsafeModelsDirectory;
        public string Namespace { get; set; } = Defaults.Namespace;
        public string ModelsDirectory { get; set; } = Defaults.ModelsDirectory;
    }
}