using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_WebApp.Application.Interfaces.IFileUploadService;
using Udemy_WebApp.Application.Interfaces.IRepository;

namespace Udemy_WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _filesUploadService;
        private readonly IUnitOfWork _unitOfWork;
        public FileUploadController(IFileUploadService filesUploadService, IUnitOfWork unitOfWork)
        {
            _filesUploadService = filesUploadService;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Uploads an image file.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <param name="directory">The image directory to upload.</param>
        /// <returns>A response indicating whether the upload was successful and the name of the image.</returns>
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file, string directory)
        {
            // Upload the image 
            string imageName = await _filesUploadService.UploadImage(file, directory);
            // Return the name of the uploaded image in a BaseResponse object
            return Ok("Image uploaded successfully");
        }

        /// <summary>
        /// Retrieves an image file by its name.
        /// </summary>
        /// <param name="imageName">The name of the image file to retrieve.</param>
        [AllowAnonymous]
        [HttpGet("Images")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            // Retrieve the image using the files service
            var imageStream = await _filesUploadService.GetFile(imageName);

            if (imageStream == null)
            {
                return NotFound();
            }

            // Return the image as a JPEG file
            return File(imageStream, "image/jpeg");
        }

        /// <summary>
        /// API endpoint to upload a video file.
        /// </summary>
        /// <param name="file">The video file to upload.</param>
        /// <param name="directory">The video directory to upload.</param>
        /// <returns>A response indicating whether the upload was successful and the name of the video.</returns>
        [HttpPost("UploadVideo")]
        public async Task<IActionResult> UploadVideo(IFormFile file, string directory)
        {
            string videoName = await _filesUploadService.UploadVideo(file, directory);
            return Ok("Successfully uploaded video.");
        }
    }
}
