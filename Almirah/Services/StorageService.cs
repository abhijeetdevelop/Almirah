using Almirah.Models;
using Almirah.Services.Interfaces;
using Azure.Storage.Blobs;

namespace Almirah.Services;

public class StorageService : IStorageService
{
    private readonly string _connectionString = "";
    private readonly string _containerName = "";

    public async Task<List<FileItem>> GetFilesAsync()
    {
        var files = new List<FileItem>();

        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        await foreach (var blobItem in containerClient.GetBlobsAsync())
            files.Add(new FileItem { Name = blobItem.Name });

        return files;
    }

    public Task<bool> UploadFileAsync(FileItem file)
    {
        // Upload file logic here
        return Task.FromResult(true);
    }

    public Task<bool> DownloadFileAsync(FileItem file)
    {
        // Download file logic here
        return Task.FromResult(true);
    }
}