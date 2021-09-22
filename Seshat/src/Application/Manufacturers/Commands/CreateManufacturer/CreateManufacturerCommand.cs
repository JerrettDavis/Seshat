using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Manufacturers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.Manufacturers.Commands.CreateManufacturer
{
    public class CreateManufacturerCommand : IRequest<ManufacturerDto>
    {
        public CreateManufacturerCommand(ManufacturerInputModel model)
        {
            Model = model;
        }

        public ManufacturerInputModel Model { get; }
    }

    [UsedImplicitly]
    public class CreateManufacturerCommandHandler :
        IRequestHandler<CreateManufacturerCommand, ManufacturerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateManufacturerCommandHandler(
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManufacturerDto> Handle(
            CreateManufacturerCommand request, 
            CancellationToken cancellationToken)
        {
            var manufacturer = new Manufacturer(request.Model.Name);

            await _context.Manufacturers.AddAsync(
                manufacturer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ManufacturerDto>(manufacturer);
        }
    }
}