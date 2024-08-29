using Azure;
using Azure.Data.Tables;

namespace abcRetail.Models
{
    public class CustomerEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}