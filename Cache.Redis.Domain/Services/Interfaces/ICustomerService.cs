using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;

namespace Cache.Redis.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> StringSetAsync(Customer customer, CancellationToken cancellationToken);
        Task<Customer> StringGetAsync(Guid key, CancellationToken cancellationToken);
        Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken);
    }
}