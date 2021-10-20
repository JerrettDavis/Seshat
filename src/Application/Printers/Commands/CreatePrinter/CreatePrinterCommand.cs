using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Seshat.Application.Common.Extensions;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Printers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.Printers.Commands.CreatePrinter
{
    public class CreatePrinterCommand : IRequest<PrinterDto>
    {
        public CreatePrinterCommand(PrinterInputModel model)
        {
            Model = model;
        }
        
        public PrinterInputModel Model { get; }
    }

    public class CreatePrinterCommandHandler :
        IRequestHandler<CreatePrinterCommand, PrinterDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePrinterCommandHandler(
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrinterDto> Handle(
            CreatePrinterCommand request, 
            CancellationToken cancellationToken)
        {
            var manufacturer = await _context
                .Manufacturers
                .PublicEntitySingleAsync(
                    request.Model.ManufacturerId, 
                    cancellationToken);

            var printer = new Printer(manufacturer, request.Model.Model);

            await _context.Printers.AddAsync(printer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PrinterDto>(printer);
        }
    }
}