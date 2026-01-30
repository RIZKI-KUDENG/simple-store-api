using SimpleStoreApi.Models;
using SimpleStoreApi.Data;
using Microsoft.EntityFrameworkCore;

namespace SimpleStoreApi.Services;

public class ProductsService
{
    private readonly AppDbContext _context;
    public ProductsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }
    public async Task<Product> Get(int id)
    {
        var product = await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);
     if (product == null)
        {
             throw new Exception("Product not found");
        }
        return product;
    }
    public async Task<Product> Add(Product product)
    {
        var categoryExisting = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
        if (!categoryExisting)
        {
            throw new Exception("Category not found");
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}