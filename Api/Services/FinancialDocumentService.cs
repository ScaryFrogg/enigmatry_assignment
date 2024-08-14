using Api.Dto;
using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services;

public class FinancialDocumentService : IFinancialDocumentService
{
    private readonly IFinancialDocumentRepository _financialDocumentRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ITenantRepository _tenantRepository;

    public FinancialDocumentService(IFinancialDocumentRepository financialDocumentRepository, IClientRepository clientRepository, ITenantRepository tenantRepository)
    {
        _financialDocumentRepository = financialDocumentRepository;
        _clientRepository = clientRepository;
        _tenantRepository = tenantRepository;
    }

    public bool IsTennantWhitelisted(Guid tenantId)
    {
        return _tenantRepository.GetById(tenantId)?.IsWhitelisted ?? false;
    }

    public (Guid, long)? GetClientId(Guid documentId)
    {
        return _financialDocumentRepository.GetClientInfo(documentId);
    }

    public FinancialDocument? GetFinancialDocument(Guid documentId)
    {
        return _financialDocumentRepository.GetById(documentId);
    }

    public Company GetCompanyInfo(long vatNumber)
    {
        return _clientRepository.GetCompanyInfo(vatNumber);
    }

    public bool IsClientWhitelisted(Guid tenantId, Guid clientId)
    {
        return _clientRepository.GetClientWhitelisting(clientId, tenantId) != null;
    }
}