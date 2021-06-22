using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;

namespace Cache.Redis.Domain.Services.Interfaces
{
    public interface IAddressService
    {
        Task<bool> StringSetAsync(Address address, CancellationToken cancellationToken);
        Task<Address> StringGetAsync(Guid key, CancellationToken cancellationToken);
    }
}