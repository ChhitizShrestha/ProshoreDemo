using Azure.Data.Tables;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using WebAppDemo.ServiceDefaults.NewFolder;
using WebAppDemo.WebApp.Controllers;
using WebAppDemo.WebApp.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Use DefaultAzureCredential to authenticate using Managed Identity
var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });

// Load Azure App Configuration
//builder.Configuration.AddAzureAppConfiguration(options =>
//{
//    options.Connect(new Uri(builder.Configuration["AzureAppConfiguration:Endpoint"]), credential);
//});

var appConfigConnectionString = builder.Configuration["AzureAppConfiguration:ConnectionString"];


// Load Azure Key Vault Secrets
var keyVaultEndpoint = builder.Configuration["KeyVault:Vault"];
if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
	builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), credential);
}

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appConfigConnectionString)
           .Select("TestApp:*", LabelFilter.Null) 
           .ConfigureRefresh(refreshOptions =>
                refreshOptions.Register("TestApp:Settings:RefreshAllKeys", refreshAll: true).SetCacheExpiration(TimeSpan.FromMinutes(1)));
    options.UseFeatureFlags();

});
builder.Services.Configure<Settings>(builder.Configuration.GetSection("TestApp:Settings"));


builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddAzureAppConfiguration();

// Add feature management to the container of services.
builder.Services.AddFeatureManagement()
     .AddFeatureFilter<LocalEnvironmentFeatureFilter>()
     .AddFeatureFilter<BrowserFeatureFilter>();





builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));
builder.Services.AddSingleton(x => new ShareClient(builder.Configuration.GetConnectionString("AzureBlobStorage"), "proshorefileshare"));
builder.Services.AddSingleton(x => new ShareServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));
builder.Services.AddSingleton(x => new QueueServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));
builder.Services.AddSingleton(x => new TableServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));


builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IQueueStorageService, QueueStorageService>();
builder.Services.AddScoped<ITableStorageService, TableStorageService>();


builder.Services.AddScoped<IHeavyComputationService, HeavyComputationService>();
builder.Services.AddScoped<ILongRunningService, LongRunningService>();
// Add IHttpContextAccessor to the services
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAzureAppConfiguration( );
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
