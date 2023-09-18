using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDQWalksAPI.Models.Domain;
using SDQWalksAPI.Models.Dtos;
using SDQWalksAPI.Repositories;

namespace SDQWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomain = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription
                };

                await imageRepository.Upload(imageDomain);
                return Ok(imageDomain);
            }

            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtentions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "unsuported file extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "filee size more than 10MB, please upload amaller size file/");
            }
        }
    }
}
