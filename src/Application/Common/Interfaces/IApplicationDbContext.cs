using Seshat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Seshat.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Printer> Printers { get; set; }
        DbSet<User> UserRecords { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}