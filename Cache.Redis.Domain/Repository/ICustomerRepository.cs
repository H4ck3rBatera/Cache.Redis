using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;

namespace Cache.Redis.Domain.Repository
{
    public interface ICustomerRepository
    {
        Task<bool> RegisterAsync(Customer customer, CancellationToken cancellationToken);
    }
}