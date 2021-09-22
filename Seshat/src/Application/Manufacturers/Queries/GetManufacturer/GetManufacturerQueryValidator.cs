using FluentValidation;
using JetBrains.Annotations;
using Seshat.Application.Manufacturers.Services;

namespace Seshat.Application.Manufacturers.Queries.GetManufacturer
{
    [UsedImplicitly]
    public class GetManufacturerQueryValidator : 
        AbstractValidator<GetManufacturerQuery>
    {
        public GetManufacturerQueryValidator(IManufacturerValidators validators)
        {
            RuleFor(m => m.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(m => m.Id)
                .MustAsync(validators.ManufacturerExists)
                .WithMessage("Manufacturer does not exist!")
                .When(e => !string.IsNullOrWhiteSpace(e.Id));

        }
    }
}