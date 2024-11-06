using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDemo.ServiceDefaults.NewFolder
{
	public interface IDataLakeStorageService
	{
		Task<Stream> DownloadFileAsync(string filePath);
		Task UploadFileAsync(Stream fileStream, string filePath);
	}

	public class DataLakeStorageService : IDataLakeStorageService
	{
		private readonly DataLakeServiceClient _dataLakeServiceClient;
		private readonly string _fileSystemName = "sample-file-system";

		public DataLakeStorageService(DataLakeServiceClient dataLakeServiceClient)
		{
			_dataLakeServiceClient = dataLakeServiceClient;
		}

		public async Task UploadFileAsync(Stream fileStream, string filePath)
		{
			var fileSystemClient = _dataLakeServiceClient.GetFileSystemClient(_fileSystemName);
			await fileSystemClient.CreateIfNotExistsAsync();
			var fileClient = fileSystemClient.GetFileClient(filePath);
			await fileClient.UploadAsync(fileStream, overwrite: true);
		}

		public async Task<Stream> DownloadFileAsync(string filePath)
		{
			var fileSystemClient = _dataLakeServiceClient.GetFileSystemClient(_fileSystemName);
			var fileClient = fileSystemClient.GetFileClient(filePath);
			var response = await fileClient.ReadAsync();
			return response.Value.Content;
		}
	}
}
