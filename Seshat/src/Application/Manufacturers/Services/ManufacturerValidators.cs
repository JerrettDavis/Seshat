using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seshat.Application.Common.Extensions;
using Seshat.Application.Common.Interfaces;

namespace Seshat.Application.Manufacturers.Services
{
    public class ManufacturerValidators : IManufacturerValidators
    {
        private readonly IApplicationDbContext _context;

        public ManufacturerValidators(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> ManufacturerExists(
            string id,
            CancellationToken cancellationToken) =>
            _context.Manufacturers.PublicEntityExistsAsync(id, cancellationToken);

        public Task<bool> NameUnique(
            string name,
            CancellationToken cancellationToken)
        {
            name = name.ToLowerInvariant();

            return _context.Manufacturers
                .AllAsync(m => m.Name.ToLower() != name, cancellationToken);
        }
            
    }
}