using FluentValidation;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using System.IO;

namespace Hy.Modeller.Core.Validators
{
    public class ContextValidator : AbstractValidator<IContext>
    {
        public ContextValidator()
        {
            RuleFor(x => x.TargetFolder).NotNull().Must(x => Directory.Exists(x)).WithMessage(m => $"No target folder named '{m.GeneratorConfiguration.Target}' was found in '{m.GeneratorConfiguration.LocalFolder}'.");
            RuleFor(x => x.Generator).NotNull();
            RuleFor(x => x.Module).NotNull().SetValidator(new ModuleValidator()).When(x => !string.IsNullOrEmpty(x.GeneratorConfiguration.SourceModel));
            RuleFor(x => x.Settings).NotNull().SetValidator(new SettingsValidator()).When(x => !string.IsNullOrEmpty(x.GeneratorConfiguration.SettingsFile));

            RuleFor(x => x.GeneratorConfiguration).SetValidator(new GeneratorConfigurationValidator());
        }
    }

    public class SettingsValidator : AbstractValidator<ISettings>
    {
        public SettingsValidator()
        {

        }
    }

    public class ModuleValidator : AbstractValidator<Module>
    {
        public ModuleValidator()
        {

        }
    }
}
