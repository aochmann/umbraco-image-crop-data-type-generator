using ImageCropDataTypeGenerator.Core.Interfaces;
using ImageCropDataTypeGenerator.Core.Models;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace ImageCropDataTypeGenerator.Umbraco.Components
{
    public class DataTypeEventsComponent : IComponent
    {
        private readonly IImageCropDataTypeGenerator _imageCropDataTypeGenerator;
        private readonly IDataTypeService _dataTypeService;

        public DataTypeEventsComponent(
            IImageCropDataTypeGenerator imageCropDataTypeGenerator,
            IDataTypeService dataTypeService)
        {
            _imageCropDataTypeGenerator = imageCropDataTypeGenerator;
            _dataTypeService = dataTypeService;
        }

        public void Initialize()
        {
            DataTypeService.Saved += On_Saved;
            DataTypeService.Deleted += On_Deleted;
        }

        public void Terminate()
        {
            DataTypeService.Saved -= On_Saved;
            DataTypeService.Deleted -= On_Deleted;
        }

        private void On_Deleted(IDataTypeService sender, DeleteEventArgs<IDataType> e)
        {
        }

        private void On_Saved(IDataTypeService sender, SaveEventArgs<IDataType> e)
        {
            var imageCroppers = e.SavedEntities
                .Where(x => x.EditorAlias == Constants.PropertyEditors.Aliases.ImageCropper)
                .ToArray();

            var cropDetails = imageCroppers
                .SelectMany(imageCropper => (imageCropper.Configuration as ImageCropperConfiguration)
                    .Crops
                    .Select(crop => new ImageCropDetails
                    {
                        ImageCropperName = imageCropper.Name,
                        Alias = crop.Alias,
                        Width = crop.Width,
                        Height = crop.Height
                    }).ToArray());

            var output = _imageCropDataTypeGenerator.Generate(cropDetails, "MyTest.App");
        }
    }
}