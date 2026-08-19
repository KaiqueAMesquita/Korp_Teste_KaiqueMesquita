using StockApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace StockApi.Data;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Product{get;set;}

    
}