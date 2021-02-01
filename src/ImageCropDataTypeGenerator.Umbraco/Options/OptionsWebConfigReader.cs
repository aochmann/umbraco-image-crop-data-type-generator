using ImageCropDataTypeGenerator.Umbraco.Extensions;
using ImageCropDataTypeGenerator.Umbraco.Helpers;
using System;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using System.Web.Hosting;
using Umbraco.Core;

namespace ImageCropDataTypeGenerator.Umbraco.Options
{
    internal class OptionsWebConfigReader
    {
        private const string AppSettingsPrefix = "ImageCropDataTypeGenerator";

        public static void ConfigureOptions(Options options)
        {
            options.Enable = GetSetting<Options, bool>(
                x => x.Enable,
                options.Enable);

            options.AcceptUnsafeModelsDirectory = GetSetting<Options, bool>(
                x => x.AcceptUnsafeModelsDirectory,
                options.AcceptUnsafeModelsDirectory);

            options.Namespace = GetSetting<Options, string>(
                x => x.Namespace,
                options.Namespace);

            var directory = GetSetting<Options, string>(x => x.ModelsDirectory);
            options.ModelsDirectory = GetModelsDirectory(directory, options.AcceptUnsafeModelsDirectory);
        }

        private static TReturnType GetSetting<TModel, TReturnType>(Expression<Func<TModel, object>> selector,
            TReturnType defaultValue = default)
            => AppSettings.Get($"{AppSettingsPrefix}.{ExpressionHelper.GetNameFromMemberExpression(selector.Body)}",
                defaultValue);

        internal static string GetModelsDirectory(string config, bool acceptUnsafe)
        {
            if (!config.HasValue())
            {
                return HostingEnvironment.IsHosted
                    ? HostingEnvironment.MapPath(config)
                    : config.TrimStart("~/");
            }

            var root = HostingEnvironment.IsHosted
                ? HostingEnvironment.MapPath("~/")
                : Directory.GetCurrentDirectory();

            if (!root.HasValue())
            {
                throw new ConfigurationErrorsException("Could not determine root directory.");
            }

            if (!Path.IsPathRooted(root))
            {
                throw new ConfigurationErrorsException($"Root is not rooted \"{root}\".");
            }

            if (!config.StartsWith("~/"))
            {
                return acceptUnsafe
                    ? Path.GetFullPath(config)
                    : throw new ConfigurationErrorsException($"Invalid models directory \"{config}\".");
            }

            var dir = Path.Combine(root, config.TrimStart("~/"));

            dir = Path.GetFullPath(dir);
            root = Path.GetFullPath(root);

            return dir.StartsWith(root) || acceptUnsafe
                ? dir
                : throw new ConfigurationErrorsException($"Invalid models directory \"{config}\".");
        }
    }
}