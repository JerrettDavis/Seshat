using Seshat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Seshat.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Printer> Printers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}