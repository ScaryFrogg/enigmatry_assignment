using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services;

public class FinancialDocumentService : IFinancialDocumentService
{
    private readonly IFinancialDocumentRepository _financialDocumentRepository;

    public FinancialDocumentService(IFinancialDocumentRepository financialDocumentRepository = null)
    {
        _financialDocumentRepository = financialDocumentRepository;
    }

    public FinancialDocument? GetFinancialDocument(Guid DocumentId)
    {
        return _financialDocumentRepository.GetDocumentWithClientData(DocumentId);
    }

}
