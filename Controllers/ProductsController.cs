using Microsoft.AspNetCore.Mvc;
using SimpleStoreApi.Models;
using SimpleStoreApi.Services;


namespace SimpleStoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsService _service;

    public ProductsController(ProductsService service)
    {
        _service = service;
    }

[HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _service.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _service.Get(id);
        if (product == null) return NotFound(); 
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {

        var result = await _service.Add(product);
        
        if (result == null) 
        {
            return BadRequest("Category ID tidak ditemukan");
        }

        return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var isDeleted = await _service.Delete(id);
        if (!isDeleted) return NotFound();
        
        return NoContent(); 
    }
}