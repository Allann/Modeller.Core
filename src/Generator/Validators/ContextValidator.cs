using Hy.Modeller.Interfaces;
using FluentValidation;

namespace Hy.Modeller.Generator.Validators
{
    public class ContextValidator : AbstractValidator<IContext>
    {
        public ContextValidator()
        {
            RuleFor(x => x.Generator).NotNull();
            RuleFor(x => x.Module).NotNull();
        }
    }
}
