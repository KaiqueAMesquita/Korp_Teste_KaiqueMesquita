using BillingApi.DTOs;
using BillingApi.Entities;
using BillingApi.Repositories;
using BillingApi.Clients;

namespace BillingApi.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository repository;
    private readonly IStockClient stockClient;

    public InvoiceService(
        IInvoiceRepository _repository,
        IStockClient _stockClient)
    {
        repository = _repository;
        stockClient = _stockClient;
    }

    public async Task<InvoiceDto> create(CreateInvoiceDto dto)
    {
        if (dto.Items == null || dto.Items.Count == 0)
            throw new ArgumentException("A nota deve possuir pelo menos um produto.");

        if (dto.Items.Any(item => item.Quantity <= 0))
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            Number = await repository.GetNextNumber(),
            Status = Enums.InvoiceStatus.Opened,
            CreatedAt = DateTime.UtcNow,
            Items = new List<InvoiceItem>()
        };

        foreach (var itemDto in dto.Items)
        {
            var product = await stockClient.getProductById(itemDto.ProductId);

            if (product == null)
                throw new KeyNotFoundException(
                    $"Produto {itemDto.ProductId} não encontrado."
                );

            var item = new InvoiceItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ProductCode = product.Code,
                ProductDescription = product.Description,
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
            throw new KeyNotFoundException("Nota não encontrada.");
        }

        return MapToDto(invoice);
    }

    public async Task<InvoiceDto?> update(
        Guid id,
        UpdateInvoiceDto dto)
    {
        var invoice = await repository.getById(id);

        if (invoice == null)
        {
            throw new KeyNotFoundException("Nota não encontrada.");
        }

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

public async Task<InvoiceDto?> print(Guid id)
{
    var invoice = await repository.getById(id);

    if (invoice == null)
        throw new KeyNotFoundException("Nota não encontrada.");

    if (invoice.Status != Enums.InvoiceStatus.Opened)
        throw new InvalidOperationException("A nota já está fechada.");

    var debitDto = new DebitStockRequestDto
    {
        Items = invoice.Items.Select(item => new DebitStockItemDto
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity
        }).ToList()
    };

    await stockClient.debit(debitDto);

    invoice.Status = Enums.InvoiceStatus.Closed;

    await repository.saveChanges();

    return MapToDto(invoice);
}
}