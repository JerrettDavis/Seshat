using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Seshat.Application.Common.Extensions;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Printers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.Printers.Commands.UpdatePrinter
{
    public class UpdatePrinterCommand : IRequest<PrinterDto>
    {
        public UpdatePrinterCommand(
            string printerId, 
            PrinterInputModel model)
        {
            PrinterId = printerId;
            Model = model;
        }
        
        public string PrinterId { get; }
        public PrinterInputModel Model { get; }
    }

    public class UpdatePrinterCommandHandler :
        IRequestHandler<UpdatePrinterCommand, PrinterDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePrinterCommandHandler(
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrinterDto> Handle(
            UpdatePrinterCommand request, 
            CancellationToken cancellationToken)
        {
            var printer = await _context
                .Printers
                .Include(p => p.Manufacturer)
                .PublicEntitySingleAsync(
                    request.PrinterId,
                    cancellationToken);

            await SetManufacturerIfDifferent(
                printer, 
                request.Model.ManufacturerId, 
                cancellationToken);

            printer.Model = request.Model.Model;

            return _mapper.Map<PrinterDto>(printer);
        }

        private async Task SetManufacturerIfDifferent(
            Printer printer,
            string manufacturerId,
            CancellationToken cancellationToken)
        {
            if (printer.Manufacturer.IsEntity(manufacturerId))
                return;

            var manufacturer = await _context
                .Manufacturers
                .PublicEntitySingleAsync(manufacturerId, cancellationToken);

            printer.Manufacturer = manufacturer;
        } 
    }
}