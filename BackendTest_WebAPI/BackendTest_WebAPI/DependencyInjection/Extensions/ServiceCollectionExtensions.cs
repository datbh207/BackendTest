using BackendTest_WebAPI.Abstractions.Repositories;
using BackendTest_WebAPI.Repositories;
using BackendTest_WebAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_WebAPI.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings")));


    }



    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddScoped(typeof(IProductRepository), typeof(ProductRepository))
        ;
}
