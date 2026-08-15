using StockApi.Entities;

namespace StockApi.Repositories;

public interface IProductRepository
{
    Task<List<Product>> getAll();

    Task<Product> getById(int id);

    Task<Product?> create(Product product);

    Task saveChanges();

}