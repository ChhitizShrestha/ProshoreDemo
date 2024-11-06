using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.ServiceDefaults.NewFolder;

namespace WebAppDemo.WebApp.Controllers
{
    public class BlobStorageController : Controller
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _storageAccountName = "proshorestorage";
        private readonly string _containerName = "testproq";
        private readonly string _sasToken = "sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2024-09-10T15:04:48Z&st=2024-09-10T07:04:48Z&spr=https&sig=Crj1mLzPLMDOnZmnRetiZdsJ6skwxePMVkzqIslbHxs%3D";
        private readonly string _baseUrl;

        public BlobStorageController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _baseUrl = $"https://{_storageAccountName}.blob.core.windows.net/{_containerName}";

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobs = new List<BlobItem>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem);
            }

            ViewBag.Blobs = blobs;




            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                var blobClient = containerClient.GetBlobClient(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return File(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType, fileName);
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveBlobProperties(string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobProperties properties = await blobClient.GetPropertiesAsync();

            ViewBag.BlobMetadata = properties.Metadata;

            ViewBag.BlobProperties = properties;
            ViewBag.BlobName = blobName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetBlobMetadata(string blobName, string key, string value)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            var metadata = new Dictionary<string, string>
    {
        { key, value }
    };

            await blobClient.SetMetadataAsync(metadata);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveContainerMetadata()
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            BlobContainerProperties properties = await containerClient.GetPropertiesAsync();
            ViewBag.ContainerMetadata = properties.Metadata;

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetContainerMetadata(string key, string value)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            var metadata = new Dictionary<string, string>
    {
        { key, value }
    };

            await containerClient.SetMetadataAsync(metadata);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> RetrieveContainerProperties()
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            BlobContainerProperties properties = await containerClient.GetPropertiesAsync();

            ViewBag.ContainerProperties = properties;
            return View();
        }

        private async Task SetBlobMetadataViaRest(string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = $"{_baseUrl}/{fileName}?comp=metadata&{_sasToken}";

                client.DefaultRequestHeaders.Add("x-ms-meta-chhitiz", "shrestha");
                //client.DefaultRequestHeaders.Add("x-ms-meta-key2", "value2");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri);
                request.Headers.Add("x-ms-version", "2021-06-08");
                request.Headers.Add("x-ms-date", DateTime.UtcNow.ToString("R"));

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }
        private async Task<string> GetBlobMetadataViaRest(string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = $"{_baseUrl}/{fileName}?comp=metadata&{_sasToken}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("x-ms-version", "2021-06-08");
                request.Headers.Add("x-ms-date", DateTime.UtcNow.ToString("R"));

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Get metadata from response headers
                var metadataHeaders = response.Headers;
                foreach (var header in metadataHeaders)
                {
                    Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                }

                return response.ToString();
            }
        }
    }

}
