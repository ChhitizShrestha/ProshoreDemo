
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebAppDemo.WebApp.Filters;
[FilterAlias("LocalEnvironment")]
public class LocalEnvironmentFeatureFilter : IFeatureFilter
{
	private readonly IHostEnvironment _hostEnvironment;

	public LocalEnvironmentFeatureFilter(IHostEnvironment hostEnvironment)
	{
		_hostEnvironment = hostEnvironment;
	}

	public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
	{
		bool isLocal = _hostEnvironment.IsDevelopment();

		return Task.FromResult(isLocal); 
	}
}

