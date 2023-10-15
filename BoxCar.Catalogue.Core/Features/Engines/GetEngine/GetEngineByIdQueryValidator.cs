using FluentValidation;

namespace BoxCar.Catalogue.Core.Features.Engines.GetEngine
{
    public class GetEngineByIdQueryValidator : AbstractValidator<GetEngineByIdQuery>
    {
        public GetEngineByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
