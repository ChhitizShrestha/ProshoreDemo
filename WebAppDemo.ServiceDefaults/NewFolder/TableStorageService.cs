using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDemo.ServiceDefaults.Model;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface ITableStorageService
	{
		Task DeleteEntityAsync(string partitionKey, string rowKey);
		Task InsertEntityAsync(User entity);
		Task<User> RetrieveEntityAsync(string partitionKey, string rowKey);
	}

	public class TableStorageService : ITableStorageService
	{
		private readonly TableServiceClient _tableServiceClient;
		private readonly string _tableName = "sampleTable";

		public TableStorageService(TableServiceClient tableServiceClient)
		{
			_tableServiceClient = tableServiceClient;
		}

		public async Task InsertEntityAsync(User entity)
		{
			var tableClient = _tableServiceClient.GetTableClient(_tableName);
			await tableClient.CreateIfNotExistsAsync();
			await tableClient.AddEntityAsync(entity);
		}

		public async Task<User> RetrieveEntityAsync(string partitionKey, string rowKey)
		{
			var tableClient = _tableServiceClient.GetTableClient(_tableName);
			return await tableClient.GetEntityAsync<User>(partitionKey, rowKey);
		}

		public async Task DeleteEntityAsync(string partitionKey, string rowKey)
		{
			var tableClient = _tableServiceClient.GetTableClient(_tableName);
			await tableClient.DeleteEntityAsync(partitionKey, rowKey);
		}
	}

}
