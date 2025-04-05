using FileService.Domain.Models;

namespace FileService.Domain.Services;
public interface IFileService
{
    Task SaveFileAsync(string fileName, byte[] fileBytes);
    Task<FileModel?> GetFileAsync(Guid guid);
    Task<byte[]?> GetFileDataAsync(Guid guid);
    Task DeleteFileAsync(Guid guid);
}
