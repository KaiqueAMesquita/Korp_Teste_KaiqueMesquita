using BillingApi.DTOs;

namespace BillingApi.Services;

public interface IInvoiceService
{
    Task<List<InvoiceDto>> getAll();

    Task<InvoiceDto?> getById(Guid id);

    Task<InvoiceDto> create(CreateInvoiceDto dto);

    Task<InvoiceDto?> update(Guid id, UpdateInvoiceDto dto);
}
