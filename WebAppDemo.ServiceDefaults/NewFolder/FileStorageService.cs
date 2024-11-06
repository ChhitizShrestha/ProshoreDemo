using Azure.Storage.Files.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface IFileStorageService
	{
		Task<Stream> DownloadFileAsync(string fileName);
		Task UploadFileAsync(Stream fileStream, string fileName);
	}

	public class FileStorageService : IFileStorageService
	{
		private readonly ShareServiceClient _shareServiceClient;
		private readonly string _shareName = "sample-share";

		public FileStorageService(ShareServiceClient shareServiceClient)
		{
			_shareServiceClient = shareServiceClient;
		}

		public async Task UploadFileAsync(Stream fileStream, string fileName)
		{
			var shareClient = _shareServiceClient.GetShareClient(_shareName);
			await shareClient.CreateIfNotExistsAsync();
			var directoryClient = shareClient.GetRootDirectoryClient();
			var fileClient = directoryClient.GetFileClient(fileName);
			await fileClient.CreateAsync(fileStream.Length);
			await fileClient.UploadAsync(fileStream);
		}

		public async Task<Stream> DownloadFileAsync(string fileName)
		{
			var shareClient = _shareServiceClient.GetShareClient(_shareName);
			var directoryClient = shareClient.GetRootDirectoryClient();
			var fileClient = directoryClient.GetFileClient(fileName);
			var downloadResponse = await fileClient.DownloadAsync();
			return downloadResponse.Value.Content;
		}
	}

}
