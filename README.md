# Cache Redis

### Packages
`<PackageReference Include="StackExchange.Redis" Version="2.2.4" />`

### Address Repository
```csharp
public class AddressRepository : IAddressRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;
        private readonly RedisOption _redisOption;

        public AddressRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer,
            IOptions<RedisOption> redisOption)
        {
            _logger = logger;
            _redisOption = redisOption.Value;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StringSetAsync(Address address, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            var json = JsonConvert.SerializeObject(address);

            return await _database
                .StringSetAsync(address.Key.ToString(), json, TimeSpan.FromSeconds(_redisOption.Expiry))
                .ConfigureAwait(false);
        }

        public async Task<Address> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            var redisValue = await _database.StringGetAsync(key.ToString()).ConfigureAwait(false);

            return redisValue.HasValue ? JsonConvert.DeserializeObject<Address>(redisValue) : null;
        }
    }
```

### Customer Repository
```csharp
public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;
        private readonly RedisOption _redisOption;

        public CustomerRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer,
            IOptions<RedisOption> redisOption)
        {
            _logger = logger;
            _redisOption = redisOption.Value;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StringSetAsync(Customer customer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            return await _database
                .StringSetAsync(customer.Key.ToString(), customer.Name, TimeSpan.FromSeconds(_redisOption.Expiry))
                .ConfigureAwait(false);
        }

        public async Task<Customer> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            var redisValue = await _database.StringGetAsync(key.ToString()).ConfigureAwait(false);

            return redisValue.HasValue ? new Customer { Name = redisValue } : null;
        }
    }
```

### General Repository
```csharp
public class GeneralRepository : IGeneralRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;

        public GeneralRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyDeleteAsync)}");

            return await _database.KeyDeleteAsync(key.ToString()).ConfigureAwait(false);
        }

        public async Task<TimeSpan?> KeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyTimeToLiveAsync)}");

            return await _database.KeyTimeToLiveAsync(key.ToString()).ConfigureAwait(false);
        }
    }
```

### Service Collection Extension
```csharp
public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<RedisOption>(configuration.GetSection("Redis"));

            var connectionString = configuration.GetSection("ConnectionStrings:Redis");
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(connectionString.Value));

            services
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IGeneralRepository, GeneralRepository>();

            return services;
        }
    }
```

### docker-compose.yml
```yaml
version: "3.5"

services:
    redis:
        container_name: Redis
        image: "bitnami/redis"
        environment:
          - REDIS_PASSWORD=Three@141592653589793
        ports:
          - "6379:6379"
```
