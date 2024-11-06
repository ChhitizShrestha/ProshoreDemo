using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Files.Shares;
using Azure;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.ServiceDefaults.NewFolder;

namespace WebAppDemo.WebApp.Controllers
{
    public class FileStorageController : Controller
    {
        private readonly ShareClient _shareClient;
        private readonly string _directoryName = "ProshoreFolder"; 

        public FileStorageController(ShareClient shareClient)
        {
            _shareClient = shareClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var directoryClient = _shareClient.GetDirectoryClient(_directoryName);
            var files = new List<string>();

            await foreach (ShareFileItem fileItem in directoryClient.GetFilesAndDirectoriesAsync())
            {
                if (!fileItem.IsDirectory)
                {
                    files.Add(fileItem.Name);
                }
            }

            ViewBag.Files = files;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var directoryClient = _shareClient.GetDirectoryClient(_directoryName);
                var fileClient = directoryClient.GetFileClient(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await fileClient.CreateAsync(file.Length);
                    await fileClient.UploadRangeAsync(new HttpRange(0, file.Length), stream);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string fileName)
        {
            var directoryClient = _shareClient.GetDirectoryClient(_directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);
            var downloadInfo = await fileClient.DownloadAsync();

            return File(downloadInfo.Value.Content, downloadInfo.Value.ContentType, fileName);
        }
    }
}
