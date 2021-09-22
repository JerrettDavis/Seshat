using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seshat.Application.Common.Extensions;
using Seshat.Application.Common.Interfaces;

namespace Seshat.Application.Printers.Services
{
    public class PrinterValidators : IPrinterValidators
    {
        private readonly IApplicationDbContext _context;

        public PrinterValidators(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> PrinterExists(
            string id,
            CancellationToken cancellationToken) =>
            _context.Printers.PublicEntityExistsAsync(id, cancellationToken);

        public Task<bool> NameUnique(
            string name,
            string manufacturerId,
            CancellationToken cancellationToken)
        {
            name = name.ToLowerInvariant();
            
            return _context.Printers
                .Where(p => p.Manufacturer.PublicIdentifier == manufacturerId)
                .AllAsync(m => m.Model.ToLower() != name, cancellationToken);
        }
    }
}