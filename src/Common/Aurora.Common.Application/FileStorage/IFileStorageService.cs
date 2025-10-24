namespace Aurora.Common.Application.FileStorage;

public interface IFileStorageService
{
    Task<string?> UploadAsync(IFormFile formFile, string folderName, string fileName);
    Task<string?> UploadAsync(IFormFile formFile, string containerName, string folderName, string fileName);
    Task DeleteAsync(string containerName, string fileName);
}