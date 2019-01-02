using FluentValidation;
using Hy.Modeller.Generator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hy.Modeller.Core.Validators
{
    public class ContextValidator : AbstractValidator<Context>
    {
        public ContextValidator()
        {
            RuleFor(x => x.ModuleFile).NotNull().NotEmpty();

        }
    }
}
