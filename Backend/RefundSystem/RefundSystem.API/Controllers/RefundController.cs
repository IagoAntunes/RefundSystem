using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.API.Requests;
using RefundSystem.Application.Dtos;
using RefundSystem.Application.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RefundSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RefundController : ControllerBase
    {
        private readonly IMapper mapper;

        public IRefundService service { get; set; }

        public RefundController(
            IMapper mapper,
            IRefundService refundService
        )
        {
            this.mapper = mapper;
            this.service = refundService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await service.GetRefundsByUser(userId);

            return Ok(result);
        }

        /// <summary>
        /// Cria um novo reembolso recebendo dados do formulário e um arquivo.
        /// </summary>
        /// <param name="request">Os dados do reembolso (Nome, CategoriaId, Valor, Arquivo).</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRefundRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createRefundDto = mapper.Map<CreateRefundDto>(request);
            var createdRefund = await service.CreateRefund(createRefundDto, userId);
            return CreatedAtAction(nameof(GetAll), new { id = createdRefund.Id }, createdRefund);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await service.DeleteRefund(userId, id);
            if(result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
