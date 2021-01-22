using ImageCropDataTypeGenerator.Core.Extensions;
using ImageCropDataTypeGenerator.Core.Interfaces;
using ImageCropDataTypeGenerator.Core.Models;
using Scriban;
using Scriban.Runtime;
using System;
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
        public static class {{ imageAliasDefinition.Name }}
        {
            public const string Alias = ""{{ imageAliasDefinition.Value }}"";
            public const int Width = {{ imageAliasDefinition.Width }};
            public const int Height = {{ imageAliasDefinition.Height }};
        }

        {{~ end ~}}
    }
}";

        private const string DefaultNamespace = "ImageCropDataTypeGenerator.Models";

        public string Generate(IEnumerable<ImageCropDetails> imageCrops, string @namespace = null)
        {
            if (!@namespace.HasValue())
            {
                @namespace = DefaultNamespace;
            }

            var imageAliasDefinitions = imageCrops.Select(imageCrop => new
            {
                Name = imageCrop.Alias.ToPascalCase(),
                Value = imageCrop.Alias,
                imageCrop.Width,
                imageCrop.Height
            }).ToArray();

            var context = new TemplateContext { MemberRenamer = member => member.Name };

            var scriptObject = new ScriptObject
            {
                ["imageAliasDefinitions"] = imageAliasDefinitions,
                ["namespace"] = @namespace
            };

            context.PushGlobal(scriptObject);

            var template = Template.Parse(ClassTemplate);
            var result = string.Empty;
            try
            {
                result = template.Render(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }
    }
}