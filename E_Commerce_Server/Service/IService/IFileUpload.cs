using Microsoft.AspNetCore.Components.Forms;

namespace E_Commerce_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFileAsync(IBrowserFile file);
        bool DeleteFile(string filePath);
    }
}
