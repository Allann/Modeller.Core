using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Generator
{
    internal class SettingsValidator : ValidatorBase
    {
        private readonly ISettings _settings;
        private readonly ISettingsLoader _loader;

        public SettingsValidator(ISettings settings, Context context, ISettingsLoader loader)
            : base(context)
        {
            _settings = settings;
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public override void Validate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Context.SettingsFile))
                {
                    Context.SetSettings(_settings);
                    return;
                }

                var settings = _loader.Load<ISettings>(Context.SettingsFile);
                if (settings == null)
                {
                    Context.SetSettings(_settings);
                }
                else
                {
                    Context.SetSettings(settings);
                }
            }
            catch (Exception ex)
            {
                AddIssue(ex.Message);
            }
        }
    }
}
