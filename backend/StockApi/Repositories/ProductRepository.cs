using Microsoft.EntityFrameworkCore;
using StockApi.Data;
using StockApi.Entities;

namespace StockApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StockDbContext context;

    public ProductRepository(StockDbContext _context)
    {
        context = _context;
    }

    public async Task<Product?> create(Product product)
    {
        context.Product.Add(product);

        await context.SaveChangesAsync();

        return product;
    }

    public async Task<List<Product>> getAll()
    {
        return await context.Product.ToListAsync();
    }

    public async Task<Product?> getById(Guid id)
    {
        return await context.Product.FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task saveChanges()
    {
        await context.SaveChangesAsync();
    }
}