using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Manufacturers.Models;

namespace Seshat.Application.Manufacturers.Queries.GetManufacturers
{
    public class GetManufacturersQuery : IRequest<IEnumerable<ManufacturerDto>>
    {
    }

    [UsedImplicitly]
    public class GetManufacturersQueryHandler : 
        IRequestHandler<GetManufacturersQuery, IEnumerable<ManufacturerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetManufacturersQueryHandler(
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManufacturerDto>> Handle(
            GetManufacturersQuery request, 
            CancellationToken cancellationToken)
        {
            return await _context.Manufacturers
                .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                .OrderBy(m => m.Name)
                .ToListAsync(cancellationToken);
        }
    }
}