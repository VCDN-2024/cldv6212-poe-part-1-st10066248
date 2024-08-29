using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace abcRetail.Controllers
{
    public class FileStorageController : Controller
    {
        private readonly FileStorageService _fileService;

        public FileStorageController(FileStorageService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string directoryName)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    await _fileService.UploadFileAsync(directoryName, file.FileName, stream);
                }
                ViewBag.Message = "File uploaded successfully.";
            }
            else
            {
                ViewBag.Message = "File upload failed. Please select a file.";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string directoryName, string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(directoryName))
            {
                ViewBag.Message = "Directory name and file name are required.";
                return View();
            }

            var stream = await _fileService.DownloadFileAsync(directoryName, fileName);
            return File(stream, "application/octet-stream", fileName);
        }
    }
}

