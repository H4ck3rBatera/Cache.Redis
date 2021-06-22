using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cache.Redis.Domain.Repository
{
    public interface IGeneralRepository
    {
        Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken);
        Task<TimeSpan?> KeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken);
    }
}