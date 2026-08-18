using BillingApi.DTOs;
using BillingApi.Entities;
using BillingApi.Repositories;
using BillingApi.Clients;

namespace BillingApi.Services;

public class InvoiceItemService : IInvoiceItemService
{
    private readonly IInvoiceItemRepository repository;
    private readonly IStockClient stockClient;

    public InvoiceItemService(
        IInvoiceItemRepository _repository,
        IStockClient _stockClient)
    {
        repository = _repository;
        stockClient = _stockClient;
    }

    public async Task<InvoiceItemDto> create(CreateInvoiceItemDto dto)
    {
        var product = await stockClient.getProductById(dto.ProductId);

        if (product == null)
            throw new KeyNotFoundException(
                $"Produto {dto.ProductId} não encontrado."
            );

        var invoiceItem = new InvoiceItem
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            ProductCode = product.Code,
            ProductDescription = product.Description,
            Quantity = dto.Quantity
        };

        var createdItem = await repository.create(invoiceItem);

        return MapToDto(createdItem);
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
            throw new KeyNotFoundException("Item da nota não encontrado.");
        }

        return MapToDto(item);
    }

    public async Task<InvoiceItemDto?> update(Guid id, CreateInvoiceItemDto dto)
    {
        var item = await repository.getById(id);

        if (item == null)
        {
            throw new KeyNotFoundException("Item da nota não encontrado.");
        }

        var product = await stockClient.getProductById(dto.ProductId);

        if (product == null)
            throw new KeyNotFoundException(
                $"Produto {dto.ProductId} não encontrado."
            );

        item.ProductId = product.Id;
        item.ProductCode = product.Code;
        item.ProductDescription = product.Description;
        item.Quantity = dto.Quantity;

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
