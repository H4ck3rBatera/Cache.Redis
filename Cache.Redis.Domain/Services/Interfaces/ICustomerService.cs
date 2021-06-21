using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;

namespace Cache.Redis.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> RegisterAsync(Customer customer, CancellationToken cancellationToken);
    }
}