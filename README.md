# Umbraco Image Crop DataType Generator #

[![NuGet Version](https://img.shields.io/nuget/v/ImageCropDataTypeGenerator.Umbraco)](https://www.nuget.org/packages/ImageCropDataTypeGenerator.Umbraco/)

Package that will help working with image aliases, width, height without manual declaring it as constant or queering for getting dedicated image alias data. Will work very similar to Models Builder and generating class to specified path.

Technology used: scriban (template engine).

## Configuration

To enable working with this generator, you need to include some app settings:

Required settings:
```xml
    <add key="ImageCropDataTypeGenerator.Enable" value="true" />
```

Optional settings:
```xml
    <add key="ImageCropDataTypeGenerator.Namespace" value="SomeProject.Core.Models.Generated" />
    <add key="ImageCropDataTypeGenerator.AcceptUnsafeModelsDirectory" value="true" />
    <add key="ImageCropDataTypeGenerator.ModelsDirectory" value="~/../SomeProject.Core/Models/Generated" />
```

## Example

In backoffice after (re)saving any **DataType** will (re)generate `ImageCropDefinition.generated.cs` file.

## Example of generated class

``` csharp
namespace CustomNamespace.SomeExample.App
{
    public static class ImageCropDefinition
    {
        public static class ImageCropper
        {
            public static class SomeTest
            {
                public const string Alias = "someTest";
                public const int Width = 500;
                public const int Height = 500;
            }

            public static class SomeAnotherAlias
            {
                public const string Alias = "some-another-alias";
                public const int Width = 100;
                public const int Height = 50;
            }

        }

        public static class ImageCropper2
        {
            public static class NewImageCropper
            {
                public const string Alias = "new-image-cropper";
                public const int Width = 20;
                public const int Height = 40;
            }

        }

    }
}
```
