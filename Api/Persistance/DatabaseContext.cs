using Api.Persistance.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistance;

public class DatabaseContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<VatRegistration> VatRegistrations { get; set; }
    public DbSet<FinancialDocument> FinancialDocuments { get; set; }
    public DbSet<ClientWhitelisting> ClientWhitelistings { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration can go here
    }
}