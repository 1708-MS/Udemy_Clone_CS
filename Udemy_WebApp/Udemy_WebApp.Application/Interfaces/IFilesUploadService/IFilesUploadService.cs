using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Application.Interfaces.IFilesUploadService
{
    public interface IFilesUploadService
    {
        Task<byte[]> GetFile(string filesName);
        Task<string> UploadImage(IFormFile file, string directory);
        Task<string> UploadVideo(IFormFile file, string directory);
        Task DeleteAsync(string path);
    }
}
