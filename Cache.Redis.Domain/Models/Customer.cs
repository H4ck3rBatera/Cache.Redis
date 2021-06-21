using System;

namespace Cache.Redis.Domain.Models
{
    public class Customer
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
    }
}