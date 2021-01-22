using ImageCropDataTypeGenerator.Core.Models;
using System.Collections.Generic;

namespace ImageCropDataTypeGenerator.Core.Interfaces
{
    public interface IImageCropDataTypeGenerator
    {
        string Generate(IEnumerable<ImageCropDetails> imageCrops, string @namespace = null);
    }
}