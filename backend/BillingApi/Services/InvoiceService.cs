using BillingApi.DTOs;
using BillingApi.Entities;
using BillingApi.Repositories;

namespace BillingApi.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository repository;
    private readonly IInvoiceItemRepository invoiceItemRepository;

    public InvoiceService(IInvoiceRepository _repository, IInvoiceItemRepository _invoiceItemRepository)
    {
        repository = _repository;
        invoiceItemRepository = _invoiceItemRepository;
    }

    public async Task<InvoiceDto> create(CreateInvoiceDto dto)
    {
        var invoice = new Invoice
        {
            Number = dto.Number,
            Status = dto.Status,
            CreatedAt = dto.CreatedAt
        };

        foreach (var itemDto in dto.Items)
        {
            var item = new InvoiceItem
            {
                ProductId = itemDto.ProductId,
                ProductCode = itemDto.ProductCode,
                ProductDescription = itemDto.ProductDescription,
                Quantity = itemDto.Quantity,
                InvoiceId = invoice.Id
            };
            invoice.Items.Add(item);
        }

        var createdInvoice = await repository.create(invoice);

        return MapToDto(createdInvoice);
    }

    public async Task<List<InvoiceDto>> getAll()
    {
        var invoices = await repository.getAll();

        return invoices.Select(invoice => MapToDto(invoice)).ToList();
    }

    public async Task<InvoiceDto?> getById(Guid id)
    {
        var invoice = await repository.getById(id);

        if (invoice == null)
        {
            return null;
        }

        return MapToDto(invoice);
    }

    public async Task<InvoiceDto?> update(Guid id, UpdateInvoiceDto dto)
    {
        var invoice = await repository.getById(id);

        if (invoice == null)
        {
            return null;
        }

        invoice.Number = dto.Number;
        invoice.Status = dto.Status;

        await repository.saveChanges();

        return MapToDto(invoice);
    }

    private InvoiceDto MapToDto(Invoice invoice)
    {
        return new InvoiceDto
        {
            Id = invoice.Id,
            Number = invoice.Number,
            Status = invoice.Status,
            CreatedAt = invoice.CreatedAt,
            Items = invoice.Items.Select(item => new InvoiceItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductCode = item.ProductCode,
                ProductDescription = item.ProductDescription,
                Quantity = item.Quantity,
                InvoiceId = item.InvoiceId
            }).ToList()
        };
    }
}
