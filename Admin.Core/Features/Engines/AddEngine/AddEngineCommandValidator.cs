using BoxCar.Admin.Domain;
using FluentValidation;

namespace Admin.Core.Features.Engines.AddEngine
{
    public class AddEngineCommandValidator : AbstractValidator<AddEngineCommand>
    {
        public AddEngineCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name)
                .MaximumLength(250)
                .NotEmpty();
            RuleFor(p => new { p.FuelType, p.IgnitionMethod }).Must(x => ValidateCombinationOfFuelTypeAndIgnitionMethod(x.FuelType, x.IgnitionMethod));
        }

        private bool ValidateCombinationOfFuelTypeAndIgnitionMethod(FuelType fuelType, IgnitionMethod ignitionMethod)
        {
            if (ignitionMethod == IgnitionMethod.ElectricMotor && fuelType != FuelType.Electricity) return false;
            return true;
        }
    }
}
