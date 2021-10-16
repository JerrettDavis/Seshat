using System.Threading;
using System.Threading.Tasks;

namespace Seshat.Application.Printers.Services
{
    public interface IPrinterValidators
    {
        Task<bool> PrinterExists(
            string id,
            CancellationToken cancellationToken);

        Task<bool> NameUnique(string name,
            string manufacturerId,
            CancellationToken cancellationToken);
    }
}