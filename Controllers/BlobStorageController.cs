using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace abcRetail.Controllers
{
    public class BlobStorageController : Controller
    {
        private readonly BlobStorageService _blobService;

        public BlobStorageController(BlobStorageService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var containerName = "product-images";
                var blobUri = await _blobService.UploadImageAsync(file, containerName);
                ViewBag.Message = $"Image uploaded successfully. Blob URL: {blobUri}";
            }
            else
            {
                ViewBag.Message = "File upload failed. Please select a file.";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                ViewBag.Message = "File name is required.";
                return View();
            }

            var containerName = "product-images";
            var stream = await _blobService.DownloadImageAsync(containerName, fileName);
            return File(stream, "application/octet-stream", fileName);
        }
    }
}
