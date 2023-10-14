﻿using FluentValidation;

namespace BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack
{
    public class GetOptionPackByIdQueryValidator : AbstractValidator<GetOptionPackByIdQuery>
    {
        public GetOptionPackByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
