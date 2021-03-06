﻿using FluentValidation;

namespace Hy.Modeller.Domain.Validators
{
    public class IndexValidator : AbstractValidator<Domain.Index>
    {
        public IndexValidator()
        {
            RuleForEach(x => x.Fields).SetValidator(new IndexFieldValidator());
            RuleFor(x => x.Fields).NotEmpty().WithMessage(m => $"Index '{m.Name}' must have at least one field");
        }
    }
}
