using ImageCropDataTypeGenerator.Umbraco.Options;
using System;
using Umbraco.Core.Composing;

namespace ImageCropDataTypeGenerator.Umbraco.Extensions
{
    internal static class CompositionExtensions
    {
        public static Composition ConfigureOptions(this Composition composition,
            Action<Options.Options> configure)
        {
            composition.Configs.GetConfig<OptionsConfiguration>().AddConfigure(configure);
            return composition;
        }
    }
}