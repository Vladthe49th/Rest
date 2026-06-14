using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace ItemsApi.Controllers
{                                              
    [ApiController]
    [Route("api/[controller]")]


    public class BlobController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobController(
            BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;



        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var container =
                _blobServiceClient.GetBlobContainerClient("my-files");

            await container.CreateIfNotExistsAsync();

            var blob =
                container.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();

            await blob.UploadAsync(stream, true);

            return Ok("Файл успішно завантажено!");
        }


        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            var container =
                _blobServiceClient.GetBlobContainerClient("my-files");

            var files = container
                .GetBlobs()
                .Select(x => x.Name)
                .ToList();

            return Ok(files);
        }


    }

}

