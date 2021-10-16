using System.Net;
using FluentValidation;
using Seshat.Application.Manufacturers.Services;
using Seshat.Application.Printers.Services;

namespace Seshat.Application.Printers.Commands.UpdatePrinter
{
    public class UpdatePrinterCommandValidator : 
        AbstractValidator<UpdatePrinterCommand>
    {
        public UpdatePrinterCommandValidator(
            IPrinterValidators printerValidators,
            IManufacturerValidators manufacturerValidators)
        {
            RuleFor(p => p.PrinterId)
                .MustAsync(printerValidators.PrinterExists)
                .WithMessage("Printer does not exist!")
                .WithErrorCode(HttpStatusCode.NotFound.ToString());

            RuleFor(p => p.Model.ManufacturerId)
                .MustAsync(manufacturerValidators.ManufacturerExists)
                .WithMessage("Manufacturer does not exist!");

            RuleFor(p => p.Model.Model)
                .NotEmpty();
        }
    }
}