using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;

namespace Cache.Redis.Domain.Repository
{
    public interface IAddressRepository
    {
        Task<bool> StringSetAsync(Address address, CancellationToken cancellationToken);
        Task<Address> StringGetAsync(Guid key, CancellationToken cancellationToken);
    }
}