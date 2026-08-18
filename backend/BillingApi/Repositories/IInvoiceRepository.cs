using BillingApi.Entities;

namespace BillingApi.Repositories;

public interface IInvoiceRepository
{
    Task<List<Invoice>> getAll();

    Task<Invoice?> getById(Guid id);

    Task<Invoice?> create(Invoice invoice);

    Task saveChanges();

    Task<int> GetNextNumber();
}
