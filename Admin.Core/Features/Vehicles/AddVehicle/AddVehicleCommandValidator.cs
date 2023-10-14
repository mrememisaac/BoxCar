using Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using FluentValidation;

namespace Admin.Core.Features.Vehicles.AddVehicle
{
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        public AddVehicleCommandValidator(IAsyncRepository<Chassis, Guid> chassisRepository, 
            IAsyncRepository<Engine, Guid> engineRepository,
            IAsyncRepository<OptionPack, Guid> optionPackRepository)
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.ChassisId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                var chassis = await chassisRepository.GetByIdAsync(id, cancellationToken);
                return chassis != null;
            });
            RuleFor(p => p.EngineId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                var engine = await engineRepository.GetByIdAsync(id, cancellationToken);
                return engine != null;
            });
            RuleFor(p => p.OptionPackId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                var pack = await optionPackRepository.GetByIdAsync(id, cancellationToken);
                return pack != null;
            });
        }     
    }
}
