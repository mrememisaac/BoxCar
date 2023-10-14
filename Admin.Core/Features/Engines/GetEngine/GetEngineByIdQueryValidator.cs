using FluentValidation;

namespace BoxCar.Admin.Core.Features.Engines.GetEngine
{
    public class GetEngineByIdQueryValidator : AbstractValidator<GetEngineByIdQuery>
    {
        public GetEngineByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
