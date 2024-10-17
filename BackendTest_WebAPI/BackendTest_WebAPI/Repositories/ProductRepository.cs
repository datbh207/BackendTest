using BackendTest_WebAPI.Abstractions.Repositories;
using BackendTest_WebAPI.Entities;
using BackendTest_WebAPI.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_WebAPI.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    private readonly ApplicationDbContext context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        FormattableString sql = $"EXEC sp_GetAllProducts";
        return await context.Database
            .SqlQuery<Product>(sql)
            .AsNoTracking()
            .ToListAsync();
    }

    public Product? GetById(int id)
    {
        var parameter = new SqlParameter("@Id", id);
        string sql = $"EXEC sp_GetProductById @Id";

        var product = context.Database
            .SqlQueryRaw<Product>(sql, parameter)
            .AsEnumerable()
            .SingleOrDefault();
        return product;
    }

    public async Task SaveChangeAsync()
    {
        await context.SaveChangesAsync();
    }
}
