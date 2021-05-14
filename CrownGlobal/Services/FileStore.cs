using CrownGlobal.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Services
{
    public class FileStore : IFileStore
    {
        private readonly IWebHostEnvironment _env;
        public FileStore(IWebHostEnvironment env)
        {
            _env = env;
        }

        readonly string[] permittedImageExtensions = { ".jpg", ".png", ".jpeg" };
        readonly string[] permittedVideoExtensions = { ".mp4", ".mkv", ".avi", ".mov" };

        public bool ValidateImageFile(IFormFile file, out string error)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedImageExtensions.Contains(ext))
            {
                error = $"Invalid FILETYPE only '{string.Join("', '", permittedImageExtensions)}' files are allowed.";
                return false;
            }


            error = "";
            return true;
        }

        public bool ValidateVideoFile(IFormFile file, out string error)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedVideoExtensions.Contains(ext))
            {
                error = $"Invalid FILETYPE only '{string.Join("', '", permittedVideoExtensions)}' files are allowed.";
                return false;
            }

            error = "";
            return true;
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            if (formFile.Length < 1)
            {
                return "";
            }

            var uploadsfolder = "FileStore";
            var rootFolder = _env.WebRootPath;

            if (!Directory.Exists(Path.Combine(rootFolder, uploadsfolder)))
            {
                Directory.CreateDirectory(Path.Combine(rootFolder, uploadsfolder));
            }

            var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();

            var fname = Path.Combine(uploadsfolder, $"{Guid.NewGuid()}{ext}").Replace('\\', '/');

            var fullFilePath = Path.Combine(rootFolder, fname);

            using (var stream = System.IO.File.Create(fullFilePath))
            {
                await formFile.CopyToAsync(stream);

                return fname;
            }

        }
    }
}
