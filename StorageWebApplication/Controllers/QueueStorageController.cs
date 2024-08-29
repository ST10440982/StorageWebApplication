using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using StorageWebApplication.Models;

namespace StorageWebApplication.Controllers
{
    public class QueueStorageController : Controller
    {
        private readonly QueueClient _queueClient;

        public QueueStorageController()
        {
            string connectionString = "<Your_Connection_String>";
            string queueName = "orderqueue";
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(OrderProcessing order)
        {
            string message = $"Order {order.OrderId}: {order.Status}";
            await _queueClient.SendMessageAsync(message);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
