using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface IBlobStorageService
	{
		Task DeleteFileAsync(string fileName);
		Task<Stream> DownloadFileAsync(string fileName);
		Task UploadFileAsync(Stream fileStream, string fileName);
	}

	public class BlobStorageService : IBlobStorageService
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly string _containerName = "sample-container";

		public BlobStorageService(BlobServiceClient blobServiceClient)
		{
			_blobServiceClient = blobServiceClient;
		}

		public async Task UploadFileAsync(Stream fileStream, string fileName)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
			await containerClient.CreateIfNotExistsAsync();
			var blobClient = containerClient.GetBlobClient(fileName);
			await blobClient.UploadAsync(fileStream, overwrite: true);
		}

		public async Task<Stream> DownloadFileAsync(string fileName)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
			var blobClient = containerClient.GetBlobClient(fileName);
			return (await blobClient.DownloadAsync()).Value.Content;
		}

		public async Task DeleteFileAsync(string fileName)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
			var blobClient = containerClient.GetBlobClient(fileName);
			await blobClient.DeleteIfExistsAsync();
		}
	}

}
