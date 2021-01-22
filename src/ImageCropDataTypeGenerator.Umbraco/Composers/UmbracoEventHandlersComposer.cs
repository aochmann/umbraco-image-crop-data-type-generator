using ImageCropDataTypeGenerator.Umbraco.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace ImageCropDataTypeGenerator.Umbraco.Composers
{
    internal class UmbracoEventHandlersComposer : IUserComposer
    {
        public void Compose(Composition composition)
            => composition.Components()
                .Append<DataTypeEventsComponent>();
    }
}