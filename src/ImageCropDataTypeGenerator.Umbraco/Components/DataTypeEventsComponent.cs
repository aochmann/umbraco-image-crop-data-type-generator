using ImageCropDataTypeGenerator.Core.Interfaces;
using ImageCropDataTypeGenerator.Core.Models;
using ImageCropDataTypeGenerator.Umbraco.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using File = System.IO.File;

namespace ImageCropDataTypeGenerator.Umbraco.Components
{
    internal class DataTypeEventsComponent : IComponent
    {
        private readonly IImageCropDataTypeGenerator _imageCropDataTypeGenerator;
        private readonly Options.Options _options;
        private readonly IDataTypeService _dataTypeService;

        public DataTypeEventsComponent(
            IImageCropDataTypeGenerator imageCropDataTypeGenerator,
            Options.Options options,
            IDataTypeService dataTypeService)
        {
            _imageCropDataTypeGenerator = imageCropDataTypeGenerator;
            _options = options;
            _dataTypeService = dataTypeService;
        }

        public void Initialize()
        {
            if (!_options.Enable)
            {
                return;
            }

            DataTypeService.Saved += On_Saved;
            DataTypeService.Deleted += On_Deleted;
        }

        public void Terminate()
        {
            if (!_options.Enable)
            {
                return;
            }

            DataTypeService.Saved -= On_Saved;
            DataTypeService.Deleted -= On_Deleted;
        }

        private void On_Deleted(IDataTypeService sender, DeleteEventArgs<IDataType> e)
            => RecreateImageCropDefinition();

        private void On_Saved(IDataTypeService sender, SaveEventArgs<IDataType> e)
            => RecreateImageCropDefinition();

        private void RecreateImageCropDefinition()
        {
            var imageCroppers = GetAllImageCropperDataTypes();

            if (!imageCroppers.HasAny())
            {
                return;
            }

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

            var classDefinitionCode = _imageCropDataTypeGenerator.Generate(cropDetails, _options.ModelsNamespace);

            SaveFileClassOutput(classDefinitionCode, _options.ModelsDirectory);
        }

        private IEnumerable<IDataType> GetAllImageCropperDataTypes()
            => _dataTypeService.GetByEditorAlias(Constants.PropertyEditors.Aliases.ImageCropper);

        public void SaveFileClassOutput(string content, string directory)
        {
            if (!Directory.Exists(directory))
            {
                _ = Directory.CreateDirectory(directory);
            }

            var outputPath = Path.Combine(directory, "ImageCropDefinition.generated.cs");

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            File.WriteAllText(outputPath, content);
        }
    }
}