using Microsoft.Extensions.DependencyInjection;
using Api.Controllers;
using Api.Persistance;
using Api.Persistance.Repositories;
using Api.Persistance.Repositories.Interfaces;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Persistance.Model;
using Api.Persistance.Model.Enums;
namespace ApiTest;


[TestFixture]
public abstract class TestBase
{
    protected ServiceProvider _serviceProvider;

    protected FinancialDocumentController _controller;
    protected IFinancialDocumentRepository _financialDocumentRepository;
    protected ITenantRepository _tenantRepository;
    private const string _jsonData = "{\"account_number\":\"95867648\",\"balance\":42331.12,\"currency\":\"EUR\",\"transactions\":[{\"transaction_id\":\"#####\",\"amount\":166.95,\"date\":\"1/4/2015\",\"description\":\"Grocery shopping\",\"category\":\"Food & Dining\"}]}";

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
        _tenantRepository = _serviceProvider.GetRequiredService<ITenantRepository>();

        _controller = new FinancialDocumentController(
            logger,
            financialDocumentService
            );
    }

    public (Tenant, FinancialDocument) InsertData(bool isTenantWhitelisted = true, bool isClientWhitelisted = true, CompanyType companyType = CompanyType.Medium)
    {
        Client client = new Client();
        VatRegistration vat1 = new VatRegistration()
        {
            VatNumber = new Random().NextInt64(),
            RegistrationNumber = Guid.NewGuid().ToString(),
            CompanyType = companyType
        };
        client.ClientVats.Add(vat1);

        Tenant tenant = new Tenant()
        {
            IsWhitelisted = isTenantWhitelisted,
        };
        if (isClientWhitelisted)
        {
            _tenantRepository.WhitelistClient(tenant, client);
        }
        FinancialDocument doc1 = new FinancialDocument()
        {
            ClientVat = vat1,
            Tenant = tenant,
            Data = _jsonData
        };

        _financialDocumentRepository.AddAsync(doc1);
        return (tenant, doc1);
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
