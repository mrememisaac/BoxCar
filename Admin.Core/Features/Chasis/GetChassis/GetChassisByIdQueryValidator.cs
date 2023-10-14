using FluentValidation;

namespace BoxCar.Admin.Core.Features.Chasis.GetChassis
{
    public class GetChassisByIdQueryValidator : AbstractValidator<GetChassisByIdQuery>
    {
        public GetChassisByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
