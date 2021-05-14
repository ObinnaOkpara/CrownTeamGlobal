using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Interfaces
{
    public interface IFileStore
    {
        bool ValidateImageFile(IFormFile file, out string error);
        bool ValidateVideoFile(IFormFile file, out string error);
        Task<string> UploadFileAsync(IFormFile formFile);
    }
}
