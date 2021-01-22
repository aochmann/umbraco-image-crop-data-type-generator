using ImageCropDataTypeGenerator.Core.Extensions;
using ImageCropDataTypeGenerator.Core.Interfaces;
using ImageCropDataTypeGenerator.Core.Models;
using Scriban;
using Scriban.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace ImageCropDataTypeGenerator.Core
{
    public class ImageCropDataTypeGenerator : IImageCropDataTypeGenerator
    {
        private const string ClassTemplate =
@"namespace {{ namespace }}
{
    public static class ImageCropDefinition
    {
        {{~ for imageAliasDefinition in imageAliasDefinitions ~}}
        public static class {{ imageAliasDefinition.ImageCropperName }}
        {
            {{~ for crop in imageAliasDefinition.Crops ~}}
            public static class {{ crop.Name }}
            {
                public const string Alias = ""{{ crop.Value }}"";
                public const int Width = {{ crop.Width }};
                public const int Height = {{ crop.Height }};
            }

            {{~ end ~}}
        }

        {{~ end ~}}
    }
}";

        private const string DefaultNamespace = "ImageCropDataTypeGenerator.Models";

        public string Generate(IEnumerable<ImageCropDetails> crops, string @namespace = null)
        {
            if (!@namespace.HasValue())
            {
                @namespace = DefaultNamespace;
            }

            var imageCropperDefinitions = crops
                .GroupBy(x => x.ImageCropperName)
                .Select(x => new
                {
                    ImageCropperName = x.Key.ToPascalCase(),
                    Crops = x.Select(y => new
                    {
                        Name = y.Alias.ToPascalCase(),
                        Value = y.Alias,
                        y.Width,
                        y.Height
                    }).ToArray()
                }).ToArray();

            var context = new TemplateContext { MemberRenamer = member => member.Name };

            var scriptObject = new ScriptObject
            {
                ["imageAliasDefinitions"] = imageCropperDefinitions,
                ["namespace"] = @namespace
            };

            context.PushGlobal(scriptObject);

            var template = Template.Parse(ClassTemplate);
            var result = template.Render(context);

            return result;
        }
    }
}