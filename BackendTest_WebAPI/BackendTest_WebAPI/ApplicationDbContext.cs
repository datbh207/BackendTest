using BackendTest_WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_WebAPI;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }
}
