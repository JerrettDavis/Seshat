using FluentValidation;
using JetBrains.Annotations;
using Seshat.Application.Manufacturers.Services;
using Seshat.Application.Printers.Services;

namespace Seshat.Application.Printers.Commands.CreatePrinter
{
    [UsedImplicitly]
    public class CreatePrinterCommandValidator : 
        AbstractValidator<CreatePrinterCommand>
    {
        public CreatePrinterCommandValidator(
            IManufacturerValidators manufacturerValidators,
            IPrinterValidators printerValidators)
        {
            RuleFor(r => r.Model)
                .NotNull();

            RuleFor(r => r.Model.ManufacturerId)
                .MustAsync(manufacturerValidators.ManufacturerExists)
                .WithMessage("Manufacturer does not exist!");

            RuleFor(r => r.Model.Model)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.Model)
                .MustAsync((model, cancellationToken) =>
                    printerValidators.NameUnique(
                        model.Model, 
                        model.ManufacturerId, 
                        cancellationToken))
                .When(r => !string.IsNullOrWhiteSpace(r.Model.Model))
                .WithMessage("A printer with the same model name already exists for the specified manufacturer");
        }
    }
}