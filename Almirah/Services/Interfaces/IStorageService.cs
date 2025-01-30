using Almirah.Models;

namespace Almirah.Services.Interfaces;

public interface IStorageService
{
    Task<List<FileItem>> GetFilesAsync();
    Task<bool> UploadFileAsync(FileItem file);
    Task<bool> DownloadFileAsync(FileItem file);
}