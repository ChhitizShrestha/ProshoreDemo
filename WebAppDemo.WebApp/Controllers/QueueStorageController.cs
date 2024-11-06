using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.ServiceDefaults.NewFolder;

namespace WebAppDemo.WebApp.Controllers
{
    public class QueueStorageController : Controller
    {
        private readonly QueueServiceClient _queueServiceClient;
        private readonly string _queueName = "proshorequeue"; // replace with your queue name
        public QueueStorageController(QueueServiceClient queueServiceClient)
        {
            _queueServiceClient = queueServiceClient;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var queueClient = _queueServiceClient.GetQueueClient(_queueName);
            var messages = new List<QueueMessage>();

            // Retrieve multiple messages (e.g., up to 10)
            QueueMessage[] queueMessages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);

            foreach (var message in queueMessages)
            {
                messages.Add(message);
            }

            ViewBag.Messages = messages;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var queueClient = _queueServiceClient.GetQueueClient(_queueName);
                await queueClient.SendMessageAsync(message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(string messageId, string popReceipt)
        {
            if (!string.IsNullOrEmpty(messageId) && !string.IsNullOrEmpty(popReceipt))
            {
                var queueClient = _queueServiceClient.GetQueueClient(_queueName);
                await queueClient.DeleteMessageAsync(messageId, popReceipt);
            }

            return RedirectToAction("Index");
        }
    }
}
