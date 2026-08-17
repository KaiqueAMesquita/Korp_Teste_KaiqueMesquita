using BillingApi.DTOs;
using BillingApi.Entities;
using BillingApi.Repositories;

namespace BillingApi.Services;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly IInvoiceItemRepository repository;

    public InvoiceItemService(IInvoiceItemRepository _repository)
    {
        repository = _repository;
    }

    public async Task<InvoiceItemDto> create(CreateInvoiceItemDto dto)
    {
        /*
        var invoiceItem = new InvoiceItem
        {
            
            ProductId = dto.ProductId,
            ProductCode = dto.ProductCode,
            ProductDescription = dto.ProductDescription,
            Quantity = dto.Quantity
            
        };


        var createdItem = await repository.create(invoiceItem);

        return MapToDto(createdItem);
        */
        return null;
    }

    public async Task<List<InvoiceItemDto>> getAll()
    {
        var items = await repository.getAll();

        return items.Select(item => MapToDto(item)).ToList();
    }

    public async Task<InvoiceItemDto?> getById(Guid id)
    {
        var item = await repository.getById(id);

        if (item == null)
        {
            return null;
        }

        return MapToDto(item);
    }

    public async Task<InvoiceItemDto?> update(Guid id, CreateInvoiceItemDto dto)
    {
        var item = await repository.getById(id);

        if (item == null)
        {
            return null;
        }
/*
        item.ProductId = dto.ProductId;
        item.ProductCode = dto.ProductCode;
        item.ProductDescription = dto.ProductDescription;
        item.Quantity = dto.Quantity;
*/
        await repository.saveChanges();

        return MapToDto(item);
    }

    private InvoiceItemDto MapToDto(InvoiceItem item)
    {
        return new InvoiceItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            ProductCode = item.ProductCode,
            ProductDescription = item.ProductDescription,
            Quantity = item.Quantity,
            InvoiceId = item.InvoiceId
        };
    }
}
