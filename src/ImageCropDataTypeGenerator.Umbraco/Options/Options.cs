namespace ImageCropDataTypeGenerator.Umbraco.Options
{
    internal class Options
    {
        internal static class Defaults
        {
            public const bool Enable = false;
            public const bool AcceptUnsafeModelsDirectory = false;
            public const string ModelsNamespace = "ImageCropDataTypeGenerator.Models";
            public const string ModelsDirectory = "~/App_Data/Models";
        }

        public bool Enable { get; set; } = Defaults.Enable;
        public bool AcceptUnsafeModelsDirectory { get; set; } = Defaults.AcceptUnsafeModelsDirectory;
        public string ModelsNamespace { get; set; } = Defaults.ModelsNamespace;
        public string ModelsDirectory { get; set; } = Defaults.ModelsDirectory;
    }
}