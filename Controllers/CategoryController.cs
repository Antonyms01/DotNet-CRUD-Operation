using CRUD.Models;
using CRUD.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoryAsync();
            return Ok(categories);
        }

        // Get a category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // Create a new category
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var newCategory = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Categoryid }, newCategory);
        }

        // Update a category by ID
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            var success = await _categoryService.UpdateCategoryAsync(id, category);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete a category by ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
