
using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAppDemo.WebApp.Filters;
[FilterAlias("Browser")]

public class BrowserFeatureFilter : IFeatureFilter
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public BrowserFeatureFilter(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
	{
		// Get the allowed browser from the feature configuration
		var allowedBrowser = context.Parameters.GetValue<string>("AllowedBrowser");

		// Get the User-Agent header to detect the browser
		var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

		// Simple browser detection (this can be extended as needed)
		bool isBrowserAllowed = false;
		if (userAgent.Contains(allowedBrowser, StringComparison.OrdinalIgnoreCase))
		{
			isBrowserAllowed = true;
		}

		return Task.FromResult(isBrowserAllowed);
	}
}

