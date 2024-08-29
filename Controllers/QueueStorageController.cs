using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace abcRetail.Controllers
{
    public class QueueStorageController : Controller
    {
        private readonly QueueStorageService _queueService;

        public QueueStorageController(QueueStorageService queueService)
        {
            _queueService = queueService;
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await _queueService.SendMessageAsync(message);
                ViewBag.Message = "Message sent successfully.";
            }
            else
            {
                ViewBag.Message = "Please enter a message.";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReceiveMessage()
        {
            var message = await _queueService.ReceiveMessageAsync();
            if (message != null)
            {
                ViewBag.Message = $"Received message: {message}";
            }
            else
            {
                ViewBag.Message = "No messages in the queue.";
            }

            return View();
        }
    }
}

