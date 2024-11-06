using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface IHeavyComputationService
	{
		BigInteger CalculateFactorial(int n);  // Synchronous method
		Task<BigInteger> CalculateFactorialAsync(int n, CancellationToken cancellationToken);  // Asynchronous method with CancellationToken
		Task<BigInteger> CalculateFactorialWithThirdPartyAsync(int n, CancellationToken cancellationToken);
	}

	public class HeavyComputationService : IHeavyComputationService
	{
		private readonly ILogger<HeavyComputationService> _logger;

		public HeavyComputationService(ILogger<HeavyComputationService> logger)
		{
			_logger = logger;
		}
		// Synchronous method for factorial calculation
		public BigInteger CalculateFactorial(int n)
		{
			_logger.LogInformation($"Calculating factorial of {n}");
			BigInteger result = 1;
			for (int i = 1; i <= n; i++)
			{
				result *= i;
			}
			_logger.LogInformation($"Factorial of {n} is someResult");
			return result;
		}

		public async Task<BigInteger> CalculateFactorialAsync(int n, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Calculating factorial of {n}");

			var result = await Task.Run(() => CalculateFactorial(n, cancellationToken));
			_logger.LogInformation($"Factorial of {n} is someResult");
			return result;
		}
		public async Task<BigInteger> CalculateFactorialWithThirdPartyAsync(int n, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Calculating factorial of {n}");

			var restult = await CalculateFactorialWithThirdParty(n, cancellationToken);
			_logger.LogInformation($"Factorial of {n} is someResult");
			return restult;
		}
		private BigInteger CalculateFactorial(int n, CancellationToken cancellationToken)
		{
			BigInteger result = 1;
			for (int i = 1; i <= n; i++)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new TaskCanceledException("Computation was cancelled.");
				}

				result *= i;

			}
			return result;
		}
		private async Task<BigInteger> CalculateFactorialWithThirdParty(int n, CancellationToken cancellationToken)
		{
			BigInteger result = 1;
			await ThirdPartyCall(cancellationToken);
			for (int i = 1; i <= n; i++)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new TaskCanceledException("Computation was cancelled.");
				}

				result *= i;

			}
			return result;
		}
		private async Task ThirdPartyCall(CancellationToken cancellationToken)
		{
			await Task.Delay(millisecondsDelay: 5000, cancellationToken);

		}
	}

}
