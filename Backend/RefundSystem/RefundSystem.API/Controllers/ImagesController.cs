using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.Application.Services.Interfaces;
using RefundSystem.Domain.Entities;
using System.Security.Claims;

namespace RefundSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService service;

        public ImagesController(IImageService service)
        {
            this.service = service;
        }

        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetImage(Guid imageId)
        {
            var userIdFromJwt = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdFromJwt))
            {
                return Unauthorized();
            }
            var image = await service.GetImage(imageId);

            if (image == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Admin") || User.IsInRole("Approver"))
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(image.FilePath);

                return File(fileBytes, image.ContentType);
            }
            else
            {
                if (image.UserId.ToString() != userIdFromJwt)
                {
                    return NotFound();
                }


                if (string.IsNullOrEmpty(userIdFromJwt))
                {
                    return Unauthorized();
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(image.FilePath);

                return File(fileBytes, image.ContentType);
            }

        }

    }
}
