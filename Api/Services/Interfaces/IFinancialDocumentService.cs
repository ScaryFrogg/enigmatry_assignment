using Api.Dto;
using Api.Persistance.Model;

namespace Api.Services.Interfaces;

public interface IFinancialDocumentService
{
    (Guid, long)? GetClientId(Guid documentId);
    Company GetCompanyInfo(long vatNumber);
    FinancialDocument? GetFinancialDocument(Guid DocumentId);
    bool IsClientWhitelisted(Guid tenantId, Guid clientId);
    bool IsTennantWhitelisted(Guid tenantId);
}
