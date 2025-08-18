using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;

        public IRefundService service { get; set; }

        public RefundController(
            IMapper mapper,
            IRefundService refundService,
            IMemoryCache memoryCache
        )
        {
            this.mapper = mapper;
            this.service = refundService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cacheKey = $"refunds_user_{userId}";
            if (memoryCache.TryGetValue(cacheKey, out var cachedResult))
            {
                return Ok(cachedResult);
            }

            var result = await service.GetRefundsByUser(userId);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            memoryCache.Set(cacheKey, result, cacheEntryOptions);

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

            ClearUserCache(userId);

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

            ClearUserCache(userId);

            return NoContent();
        }

        private void ClearUserCache(Guid userId)
        {
            var cacheKey = $"refunds_user_{userId}";
            memoryCache.Remove(cacheKey);
        }
    }
}
