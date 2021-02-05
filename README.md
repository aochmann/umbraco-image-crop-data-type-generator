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
