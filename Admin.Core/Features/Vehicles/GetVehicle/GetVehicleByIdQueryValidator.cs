﻿using FluentValidation;

namespace Admin.Core.Features.Vehicles.GetVehicle
{
    public class GetVehicleByIdQueryValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
