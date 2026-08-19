using Microsoft.EntityFrameworkCore;
using BillingApi.Entities;

namespace BillingApi.Data;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<InvoiceItem> InvoiceItem { get; set; }
}