﻿using FluentValidation;

namespace Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisCommandValidator : AbstractValidator<AddChassisCommand>
    {
        public AddChassisCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name)
                .MaximumLength(250)
                .NotEmpty();
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
