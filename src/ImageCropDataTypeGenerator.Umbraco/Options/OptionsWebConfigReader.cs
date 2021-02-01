using ImageCropDataTypeGenerator.Umbraco.Extensions;
using ImageCropDataTypeGenerator.Umbraco.Helpers;
using System.Configuration;
using System.IO;
using System.Web.Hosting;
using Umbraco.Core;

namespace ImageCropDataTypeGenerator.Umbraco.Options
{
    internal class OptionsWebConfigReader
    {
        // the master prefix for all appSetting entries
        private const string AppSettingsPrefix = "ImageCropDataTypeGenerator";

        public static void ConfigureOptions(Options options)
        {
            options.Enable = GetSetting(
                "Enable",
                options.Enable);

            // more switches
            options.AcceptUnsafeModelsDirectory = GetSetting(
                "AcceptUnsafeModelsDirectory",
                options.AcceptUnsafeModelsDirectory);

            // strings
            options.ModelsNamespace = GetSetting(
                "Namespace",
                options.ModelsNamespace);

            // directory
            var directory = GetSetting("ModelsDirectory", "");

            if (!directory.HasValue())
            {
                options.ModelsDirectory = HostingEnvironment.IsHosted
                    ? HostingEnvironment.MapPath(options.ModelsDirectory)
                    : options.ModelsDirectory.TrimStart("~/");
            }
            else
            {
                var root = HostingEnvironment.IsHosted
                    ? HostingEnvironment.MapPath("~/")
                    : Directory.GetCurrentDirectory();

                if (!root.HasValue())
                {
                    throw new ConfigurationErrorsException("Could not determine root directory.");
                }

                // GetModelsDirectory will ensure that the path is safe
                options.ModelsDirectory = GetModelsDirectory(root, directory, options.AcceptUnsafeModelsDirectory);
            }
        }

        private static TReturnType GetSetting<TReturnType>(string name, TReturnType defaultValue = default)
            => AppSettings.Get<TReturnType>($"{AppSettingsPrefix}.{name}", defaultValue);

        internal static string GetModelsDirectory(string root, string config, bool acceptUnsafe)
        {
            if (!Path.IsPathRooted(root))
            {
                throw new ConfigurationErrorsException($"Root is not rooted \"{root}\".");
            }

            if (config.StartsWith("~/"))
            {
                var dir = Path.Combine(root, config.TrimStart("~/"));

                // sanitize - GetFullPath will take care of any relative
                // segments in path, eg '../../foo.tmp' - it may throw a SecurityException
                // if the combined path reaches illegal parts of the filesystem
                dir = Path.GetFullPath(dir);
                root = Path.GetFullPath(root);

                if (!dir.StartsWith(root) && !acceptUnsafe)
                {
                    throw new ConfigurationErrorsException($"Invalid models directory \"{config}\".");
                }

                return dir;
            }

            if (acceptUnsafe)
            {
                return Path.GetFullPath(config);
            }

            throw new ConfigurationErrorsException($"Invalid models directory \"{config}\".");
        }
    }
}