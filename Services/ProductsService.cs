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
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }
}