using BillingApi.Entities;

namespace BillingApi.Repositories;

public interface IInvoiceItemRepository
{
    Task<List<InvoiceItem>> getAll();

    Task<InvoiceItem?> getById(Guid id);

    Task<InvoiceItem?> create(InvoiceItem invoiceItem);

    Task saveChanges();
}
