using Azure;
using Azure.Data.Tables;

namespace ItemsApi.Models
{
    public class StudentEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Students";

        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}