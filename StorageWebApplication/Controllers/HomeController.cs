using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using StorageWebApplication.Models;
using System.Diagnostics;

namespace StorageWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly QueueServiceClient _queueServiceClient;

        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobServiceClient, QueueServiceClient queueServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _queueServiceClient = queueServiceClient;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UploadViewModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("image");
                var blobClient = blobContainer.GetBlobClient(model.File.FileName);

                using (var stream = model.File.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream);
                }

                var queueClient = _queueServiceClient.GetQueueClient("orderqueue");
                await queueClient.SendMessageAsync($"Uploading image {model.File.FileName}");
            }
            else
            {
                _logger.LogWarning("No file uploaded.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            var queueClient = _queueServiceClient.GetQueueClient("orderqueue");
            await queueClient.SendMessageAsync($"Processing order {orderId}");

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
