using BackendTest_WebAPI.Model;

namespace BackendTest_WebAPI.Abstractions.Repositories;

public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Product? GetById(int id);
}
