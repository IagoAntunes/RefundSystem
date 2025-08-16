using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.Application.Services.Interfaces;

namespace RefundSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {

        public IRefundService service { get; set; }

        public RefundController(
            IRefundService refundService    
        )
        {
            this.service = refundService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }


    }
}
