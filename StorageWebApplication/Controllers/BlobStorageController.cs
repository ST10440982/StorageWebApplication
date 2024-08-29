using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace StorageWebApplication.Controllers
{
    public class BlobStorageController : Controller
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "images";

        public BlobStorageController()
        {
            string connectionString = "<Your_Connection_String>";
            _blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            containerClient.CreateIfNotExists();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var blobClient = _blobServiceClient.GetBlobContainerClient(_containerName).GetBlobClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
