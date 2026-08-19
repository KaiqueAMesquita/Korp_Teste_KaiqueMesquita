using Microsoft.EntityFrameworkCore;
using BillingApi.Data;
using BillingApi.Entities;

namespace BillingApi.Repositories;

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly BillingDbContext context;

    public InvoiceItemRepository(BillingDbContext _context)
    {
        context = _context;
    }

    public async Task<InvoiceItem?> create(InvoiceItem invoiceItem)
    {
        context.InvoiceItem.Add(invoiceItem);

        await context.SaveChangesAsync();

        return invoiceItem;
    }

    public async Task<List<InvoiceItem>> getAll()
    {
        return await context.InvoiceItem.ToListAsync();
    }

    public async Task<InvoiceItem?> getById(Guid id)
    {
        return await context.InvoiceItem.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task saveChanges()
    {
        await context.SaveChangesAsync();
    }
}