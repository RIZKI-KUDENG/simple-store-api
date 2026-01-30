using Microsoft.EntityFrameworkCore;
using SimpleStoreApi.Data;
using SimpleStoreApi.Models;

namespace SimpleStoreApi.Services;

public class CategoryService
{
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Category> Add(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return false;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}