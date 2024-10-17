using BackendTest_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackendTest_WebAPI;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Product { get; set; }
}
