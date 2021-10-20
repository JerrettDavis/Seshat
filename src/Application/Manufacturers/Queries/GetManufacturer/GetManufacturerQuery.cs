using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Manufacturers.Models;

namespace Seshat.Application.Manufacturers.Queries.GetManufacturer
{
    public class GetManufacturerQuery : IRequest<ManufacturerDto>
    {
        public GetManufacturerQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    [UsedImplicitly]
    public class GetManufacturerQueryHandler :
        IRequestHandler<GetManufacturerQuery, ManufacturerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetManufacturerQueryHandler(
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ManufacturerDto> Handle(
            GetManufacturerQuery request, 
            CancellationToken cancellationToken)
        {
            return _context.Manufacturers
                .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                .SingleAsync(m => m.Id == request.Id);
        }
    }
}