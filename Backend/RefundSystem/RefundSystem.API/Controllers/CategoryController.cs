using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.API.Requests;
using RefundSystem.Application.Dtos;
using RefundSystem.Application.Services.Interfaces;

namespace RefundSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper
        )
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await categoryService.GetAllCategories();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var createCategoryDto = mapper.Map<CreateCategoryDto>(request);
            var result = await categoryService.CreateCategory(createCategoryDto);

            return CreatedAtAction(nameof(GetAllCategories), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("{categoryId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] Guid categoryId, 
            [FromBody] UpdateCategoryRequest request
        )
        {
            var updateCategoryDto = mapper.Map<UpdateCategoryDto>(request);
            var result = await categoryService.UpdateCategory(categoryId, updateCategoryDto);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        [Route("{categoryId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var result = await categoryService.DeleteCategory(categoryId);
            if (result == null) return NotFound();
            return NoContent();
        }

    }
}
