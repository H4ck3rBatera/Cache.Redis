using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cache.Redis.Domain.Services.Interfaces
{
    public interface IGeneralService
    {
        Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken);
        Task<TimeSpan?> KeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken);
    }
}