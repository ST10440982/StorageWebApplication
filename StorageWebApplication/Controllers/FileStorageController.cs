using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.Mvc;

namespace StorageWebApplication.Controllers
{
    public class FileStorageController : Controller
    {

        private readonly ShareClient _shareClient;

        public FileStorageController()
        {
            string connectionString = "<Your_Connection_String>";
            string shareName = "contracts";
            _shareClient = new ShareClient(connectionString, shareName);
            _shareClient.CreateIfNotExists();
        }

        [HttpPost]
        public async Task<IActionResult> UploadContract(IFormFile file)
        {
            var directoryClient = _shareClient.GetDirectoryClient("documents");
            var fileClient = directoryClient.GetFileClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await fileClient.CreateAsync(stream.Length);
                await fileClient.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }

    }
    
}
