using Microsoft.AspNetCore.Mvc;
using WebAppDemo.ServiceDefaults.NewFolder;

namespace WebAppDemo.WebApp.Controllers
{
	public class TaskController : Controller
	{
		private readonly IHeavyComputationService _heavyComputationService;
		private readonly ILongRunningService _longRunningService;
		private readonly ILogger<TaskController> _logger;

		public TaskController(IHeavyComputationService heavyComputationService, ILongRunningService longRunningService, ILogger<TaskController> logger)
		{
			_heavyComputationService = heavyComputationService;
			_longRunningService = longRunningService;
			_logger = logger;
		}

		public IActionResult Index() { return View(); }
		// Synchronous factorial calculation
		public IActionResult CalculateFactorialSync(int n)
		{
			_logger.LogInformation("Calculating Factorial (Sync) of {n}", n);
			var result = _heavyComputationService.CalculateFactorial(n);
			_logger.LogInformation("Factorial (Sync) of {n} is someResult", n);
			return Content($"Factorial (Sync) of {n} is 1");
		}

		// Asynchronous factorial calculation with CancellationToken
		public async Task<IActionResult> CalculateFactorialAsync(int n, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Calculating Factorial (Async) of {n}", n);
				var result = await _heavyComputationService.CalculateFactorialAsync(n, cancellationToken);
				_logger.LogInformation("Factorial (Async) of {n} is someResult", n);
				return Content($"Factorial (Async) of {n} is 1");
			}
			catch (TaskCanceledException)
			{
				_logger.LogError("Computation was cancelled.");
				return Content("Computation was cancelled.");
			}
		}

		// Asynchronous factorial calculation with CancellationToken
		public async Task<IActionResult> CalculateFactorialWithThirdPartyAsync(int n, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Calculating Factorial (Async) of {n}", n);
				var result = await _heavyComputationService.CalculateFactorialWithThirdPartyAsync(n, cancellationToken);
				_logger.LogInformation("Factorial (Async) of {n} is someResult", n);
				return Content($"Factorial (Async) of {n} is 1");
			}
			catch (TaskCanceledException)
			{
				_logger.LogError("Computation was cancelled.");
				return Content("Computation was cancelled.");
			}
		}

		// Action using async/await without CancellationToken (for I/O-bound operation)
		public async Task<IActionResult> FetchData()
		{
			_logger.LogInformation("Fetching data...");
			var result = await _longRunningService.GetDataAsync(CancellationToken.None); // No cancellation
			_logger.LogInformation("Data fetched: {result}", result);
			return Content(result);
		}

		// Action using async/await with CancellationToken for I/O-bound operation
		public async Task<IActionResult> FetchDataWithCancellation(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Fetching data...");
			var result = await _longRunningService.GetDataAsync(cancellationToken);
			_logger.LogInformation("Data fetched: {result}", result);
			return Content(result);
		}
	}
}
