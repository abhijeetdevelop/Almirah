using Almirah.Models;

namespace Almirah.Services.Interfaces;

public interface IFileSystemService
{
    Task<bool> CopyFileAsync(FileItem source, FileItem destination);
}