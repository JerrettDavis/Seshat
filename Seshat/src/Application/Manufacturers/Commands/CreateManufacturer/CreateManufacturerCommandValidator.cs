using FluentValidation;
using JetBrains.Annotations;
using Seshat.Application.Manufacturers.Services;

namespace Seshat.Application.Manufacturers.Commands.CreateManufacturer
{
    [UsedImplicitly]
    public class CreateManufacturerCommandValidator :
        AbstractValidator<CreateManufacturerCommand>
    {
        public CreateManufacturerCommandValidator(
            IManufacturerValidators validators)
        {
            RuleFor(r => r.Model)
                .NotNull();
            RuleFor(r => r.Model.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.Model.Name)
                .MustAsync(validators.NameUnique)
                .When(r => !string.IsNullOrWhiteSpace(r.Model.Name))
                .WithMessage("A manufacturer with that name already exists!");

        }
    }
}