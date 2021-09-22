using System.Threading;
using System.Threading.Tasks;

namespace Seshat.Application.Manufacturers.Services
{
    public interface IManufacturerValidators
    {
        Task<bool> ManufacturerExists(string id, CancellationToken cancellationToken);
        Task<bool> NameUnique(string name, CancellationToken cancellationToken);
    }
}