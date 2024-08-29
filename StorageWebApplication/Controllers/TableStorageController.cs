using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using StorageWebApplication.Models;

namespace StorageWebApplication.Controllers
{
    public class TableStorageController : Controller
    {
        private readonly TableClient _tableClient;

        public TableStorageController()
        {
            string connectionString = "<Your_Connection_String>";
            string tableName = "CustomerProfiles";
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerProfile(CustomerProfile profile)
        {
            await _tableClient.AddEntityAsync(profile);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
