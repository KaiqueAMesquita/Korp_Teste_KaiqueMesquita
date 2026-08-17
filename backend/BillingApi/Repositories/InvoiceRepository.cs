using Microsoft.EntityFrameworkCore;
using BillingApi.Data;
using BillingApi.Entities;

namespace BillingApi.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly BillingDbContext context;

    public InvoiceRepository(BillingDbContext _context)
    {
        context = _context;
    }

    public async Task<Invoice?> create(Invoice invoice)
    {
        context.Invoice.Add(invoice);

        await context.SaveChangesAsync();

        return invoice;
    }

    public async Task<List<Invoice>> getAll()
    {
        return await context.Invoice.Include(i => i.Items).ToListAsync();
    }

    public async Task<Invoice?> getById(Guid id)
    {
        return await context.Invoice.Include(i => i.Items).FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    public async Task saveChanges()
    {
        await context.SaveChangesAsync();
    }

    public async Task<int> GetNextNumber()
    {
        var lastNumber = await context.Invoice
            .MaxAsync(i => (int?)i.Number) ?? 0;

        return lastNumber + 1;
    }
    }