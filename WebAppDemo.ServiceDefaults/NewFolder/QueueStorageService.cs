using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface IQueueStorageService
	{
		Task<string> ReceiveMessageAsync();
		Task SendMessageAsync(string message);
	}

	public class QueueStorageService : IQueueStorageService
	{
		private readonly QueueServiceClient _queueServiceClient;
		private readonly string _queueName = "sample-queue";

		public QueueStorageService(QueueServiceClient queueServiceClient)
		{
			_queueServiceClient = queueServiceClient;
		}

		public async Task SendMessageAsync(string message)
		{
			var queueClient = _queueServiceClient.GetQueueClient(_queueName);
			await queueClient.CreateIfNotExistsAsync();
			await queueClient.SendMessageAsync(message);
		}

		public async Task<string> ReceiveMessageAsync()
		{
			var queueClient = _queueServiceClient.GetQueueClient(_queueName);
			var response = await queueClient.ReceiveMessageAsync();
			if (response.Value != null)
			{
				await queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
				return response.Value.MessageText;
			}
			return null;
		}
	}

}
