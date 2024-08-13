using Microsoft.Extensions.DependencyInjection;
using Api.Controllers;
using Api.Persistance;
using Api.Persistance.Repositories;
using Api.Persistance.Repositories.Interfaces;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ApiTest;


[TestFixture]
public abstract class TestBase
{
    protected ServiceProvider _serviceProvider;

    protected FinancialDocumentController _controller;
    protected IFinancialDocumentRepository _financialDocumentRepository;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddScoped<IFinancialDocumentService, FinancialDocumentService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IFinancialDocumentRepository, FinancialDocumentRepository>();
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        services.AddLogging();
        _serviceProvider = services.BuildServiceProvider();

        // Resolve the controller with its dependencies
        var financialDocumentService = _serviceProvider.GetRequiredService<IFinancialDocumentService>();
        var logger = _serviceProvider.GetRequiredService<ILogger<FinancialDocumentController>>();

        _financialDocumentRepository = _serviceProvider.GetRequiredService<IFinancialDocumentRepository>();
        _controller = new FinancialDocumentController(
            logger,
            financialDocumentService
            );
    }

    [TearDown]
    public void TearDown()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
