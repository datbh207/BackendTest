using BackendTest_WebAPI.Entities;

namespace BackendTest_WebAPI.Abstractions.Repositories;

public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Product? GetById(int id);
    Task SaveChangeAsync();
}
