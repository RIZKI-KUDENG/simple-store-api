using Microsoft.AspNetCore.Mvc;
using SimpleStoreApi.Models;
using SimpleStoreApi.Services;

namespace SimpleStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _service.GetAll();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _service.Get(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        var newCategory = await _service.Add(category);
        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var isDeleted = await _service.Delete(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}