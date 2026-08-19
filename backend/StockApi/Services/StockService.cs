using StockApi.DTOs;
using StockApi.Data;
using StockApi.Repositories;
namespace StockApi.Services;

public class StockService: IStockService{
    private readonly IProductRepository repository;
    private readonly StockDbContext context;

    public StockService( IProductRepository _repository,StockDbContext _context)
    {
        repository = _repository;
        context = _context;
    }

    public async Task debit(DebitStockRequestDto dto)
{
    if (dto.Items == null || dto.Items.Count == 0)
        throw new ArgumentException("Nenhum produto foi informado.");

    await using var transaction =
        await context.Database.BeginTransactionAsync();

    try
    {
        // Agrupa produtos repetidos na mesma nota
        var items = dto.Items
            .GroupBy(x => x.ProductId)
            .Select(group => new
            {
                ProductId = group.Key,
                Quantity = group.Sum(x => x.Quantity)
            })
            .ToList();

        foreach (var item in items)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var product = await repository.getById(item.ProductId);

            if (product == null)
                throw new KeyNotFoundException($"Produto {item.ProductId} não encontrado.");

            if (product.Balance < item.Quantity)
            {
                throw new InvalidOperationException(
                    $"Saldo insuficiente para o produto {product.Description}."
                );
            }

            product.Balance -= item.Quantity;
        }

        await context.SaveChangesAsync();

        await transaction.CommitAsync();
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

}