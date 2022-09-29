using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InfrastructureManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("uploadFiles")]
        public async Task<IActionResult> UploadFiles()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.Name);
                List<string> fileUrls = new List<string>();
                var files = HttpContext.Request.Form.Files;
                string uploads = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images");
                foreach (var file in files)
                {
                    string filePath = Path.Combine(uploads, userId + "XXX" + file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    fileUrls.Add(file.FileName);
                }
                return Ok(fileUrls);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
