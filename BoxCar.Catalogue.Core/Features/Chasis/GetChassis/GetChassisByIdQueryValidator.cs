using FluentValidation;

namespace BoxCar.Catalogue.Core.Features.Chasis.GetChassis
{
    public class GetChassisByIdQueryValidator : AbstractValidator<GetChassisByIdQuery>
    {
        public GetChassisByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
