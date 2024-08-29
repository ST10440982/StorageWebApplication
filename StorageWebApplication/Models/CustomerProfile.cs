using Azure.Data.Tables;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace StorageWebApplication.Models
{
    public class CustomerProfile : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}




