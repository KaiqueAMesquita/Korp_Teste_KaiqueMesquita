using BillingApi.DTOs;

namespace BillingApi.Services;

public interface IInvoiceItemService
{
    Task<List<InvoiceItemDto>> getAll();

    Task<InvoiceItemDto?> getById(Guid id);

    Task<InvoiceItemDto> create(CreateInvoiceItemDto dto);

    Task<InvoiceItemDto?> update(Guid id, CreateInvoiceItemDto dto);
}
