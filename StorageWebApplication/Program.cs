using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve connection string from configuration
var azureConnectionString = builder.Configuration.GetConnectionString("AzureStorage");

// Validate connection string
if (string.IsNullOrEmpty(azureConnectionString))
{
    throw new ArgumentNullException("AzureStorage", "Azure Storage connection string is missing.");
}

// Configure Azure Blob Storage and Queue Storage
builder.Services.AddSingleton(x => new BlobServiceClient(azureConnectionString));
builder.Services.AddSingleton(x => new QueueServiceClient(azureConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();