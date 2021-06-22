using System;

namespace Cache.Redis.Domain.Models
{
    public class Address
    {
        public Guid Key { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
    }
}