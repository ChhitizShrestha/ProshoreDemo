using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface ILongRunningService
	{
		Task<string> GetDataAsync(CancellationToken cancellationToken);
	}

	public class LongRunningService : ILongRunningService
	{
		private readonly ILogger<LongRunningService> _logger;

		public LongRunningService(ILogger<LongRunningService> logger)
		{
			_logger = logger;
		}

		public async Task<string> GetDataAsync(CancellationToken cancellationToken)
		{
			// Simulating a long-running operation (e.g., fetching from a remote API)
			try
			{
				await Task.Delay(5000, cancellationToken); // 5 second delay

				_logger.LogInformation("Data fetched successfully!");
				return "Data fetched successfully!";
			}
			catch (TaskCanceledException)
			{
				_logger.LogInformation("Operation was cancelled.");
				return "Operation was cancelled.";
			}
		}
	}
}
