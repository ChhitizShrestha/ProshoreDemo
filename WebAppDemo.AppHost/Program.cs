using Microsoft.AspNetCore.Builder;

var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.WebApplication1>("webapplication1");
//builder.AddAzureBlobClient(
//	"blobs",

//	static configureClientBuilder: clientBuilder =>
//		clientBuilder.ConfigureOptions(
//			static options => options.Diagnostics.ApplicationId = "myapp"));

//builder.AddProject<WebApplication1>("webappdemo");

builder.AddProject<Projects.WebAppDemo_WebApp>("webappdemo-webapp");
//builder.AddAzureBlobClient(
//	"blobs",

//	static configureClientBuilder: clientBuilder =>
//		clientBuilder.ConfigureOptions(
//			static options => options.Diagnostics.ApplicationId = "myapp"));

//builder.AddProject<WebApplication1>("webappdemo");

builder.Build().Run();
