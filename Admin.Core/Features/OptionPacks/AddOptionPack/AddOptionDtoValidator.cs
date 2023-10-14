﻿using FluentValidation;

namespace Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class AddOptionDtoValidator : AbstractValidator<AddOptionDto>
    {
        public AddOptionDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Value).NotEmpty();
        }
    }
}
