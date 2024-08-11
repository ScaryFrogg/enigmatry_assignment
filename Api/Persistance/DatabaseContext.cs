using Api.Persistance.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistance;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration can go here
    }
}