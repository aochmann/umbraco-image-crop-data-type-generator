using ImageCropDataTypeGenerator.Umbraco.Extensions;
using ImageCropDataTypeGenerator.Umbraco.Options;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace ImageCropDataTypeGenerator.Umbraco.Composers
{
    internal class ConfigurationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Configs.Add(() => new OptionsConfiguration());

            _ = composition.ConfigureOptions(OptionsWebConfigReader.ConfigureOptions);

            composition.Register(factory =>
            {
                var instance = factory.GetInstance<OptionsConfiguration>();
                return instance.GetOptions;
            }, Lifetime.Singleton);
        }
    }
}