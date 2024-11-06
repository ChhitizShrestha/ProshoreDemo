using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.Model
{
    public class User : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // ITableEntity implementation
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public User() { }

        public User(string partitionKey, string rowKey, string name, string email)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            Email = email;
        }
    }
}
