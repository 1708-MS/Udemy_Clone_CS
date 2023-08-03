﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.Interfaces.IFilesUploadService;
using Udemy_WebApp.Domain.Exceptions;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.FilesUploadService
{
    public class FilesUploadService : IFilesUploadService
    {
        private readonly ILogger<FilesUploadService> _logger;
        public FilesUploadService(ILogger<FilesUploadService> logger)
        {
            _logger = logger;
        }

        // Gets the absolute path to the root directory for files.
        private string PathRoot(string nameFile,string folder)
        {
            if (folder == null)
            {
                return Path.Combine(Environment.CurrentDirectory, nameFile);
            }
            else
            {
                return Path.Combine(Environment.CurrentDirectory, folder, nameFile);
            }           
        }

        // Uploads a file to the server and returns the file name.
        private async Task<string> UploadFile(IFormFile file, string directory)
        {
            try
            {
                var pathDirectory = PathRoot("Images",null);

                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }

                var path = PathRoot(Path.Combine(pathDirectory, file.FileName),null);

                // Creates a file stream and copies the uploaded file to it.
                using FileStream fileStream = new(path, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return file.FileName;
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "Upload failed: " + ex.Message);
            }
        }

        // Gets the contents of a file as a byte array.
        public async Task<byte[]> GetFile(string fileName)
        {
            // Gets the file path.
            var path = PathRoot(fileName, "Images");

            // Reads the file into a byte array and returns it.
            return await System.IO.File.ReadAllBytesAsync(path);
        }


        // Uploads an image file to the server and returns the file name.
        public async Task<string> UploadImage(IFormFile file, string directory)
        {
            // Defines the allowed file extensions for image files.
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            // Gets the extension of the uploaded file.
            string fileExtension = Path.GetExtension(file.FileName);

            // Checks if the file extension is allowed for images.
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ApiException(
                    HttpStatusCode.BadRequest,
                    $"Invalid file type. Only JPG, PNG , WEBP and GIF files are allowed."
                );
            }
            // Uploads the file and returns the file name.
            return await UploadFile(file, directory);
        }





        // Uploads a video file to the server and returns the file name.
        public async Task<string> UploadVideo(IFormFile file, string directory)
        {
            // Defines the allowed file extensions for video files.
            string[] allowedExtensions = { ".mp4", ".avi", ".mov" };

            // Gets the extension of the uploaded file.
            string fileExtension = Path.GetExtension(file.FileName);

            // Checks if the file extension is allowed for videos.
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ApiException(
                    HttpStatusCode.BadRequest,
                    $"Invalid file type. Only MP4, AVI and MOV files are allowed."
                );
            }

            // Uploads the file and returns the file name.
            return await UploadFile(file, directory);
        }



        /// <summary>
        /// Deletes the specified file asynchronously.
        /// </summary>
        /// <param name="file">The file to delete. This can be a relative or absolute path.</param>
        /// <returns>A task that represents the asynchronous file deletion operation.</returns>
        public async Task DeleteAsync(string imageName)
        {
            var fullPath = PathRoot(imageName, "Images");
            if (System.IO.File.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => System.IO.File.Delete(fullPath));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while deleting the file: {ex.Message}");
                }
            }
            else if (Directory.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => Directory.Delete(fullPath, true));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while deleting the Directory: {ex.Message}");
                }
            }
            else
            {
                _logger.LogError($"Failed to delete {imageName}");
            }
        }
    }
}
