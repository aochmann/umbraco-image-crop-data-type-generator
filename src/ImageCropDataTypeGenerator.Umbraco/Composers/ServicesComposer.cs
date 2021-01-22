using ImageCropDataTypeGenerator.Core.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace ImageCropDataTypeGenerator.Umbraco.Composers
{
    internal class ServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
            => composition.Register<IImageCropDataTypeGenerator, Core.ImageCropDataTypeGenerator>();
    }
}