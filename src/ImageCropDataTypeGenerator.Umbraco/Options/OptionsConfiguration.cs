using System;
using System.Collections.Generic;

namespace ImageCropDataTypeGenerator.Umbraco.Options
{
    internal class OptionsConfiguration
    {
        private List<Action<Options>> _configureOptions;
        private Options _modelsOptions;

        public void AddConfigure(Action<Options> configure)
            => (_configureOptions ??= new List<Action<Options>>()).Add(configure);

        public Options GetOptions
            => _modelsOptions ??= Configure(new Options());

        private Options Configure(Options modelsOptions)
        {
            if (_configureOptions == null)
            {
                return modelsOptions;
            }

            foreach (var configure in _configureOptions)
            {
                configure(modelsOptions);
            }

            return modelsOptions;
        }
    }
}