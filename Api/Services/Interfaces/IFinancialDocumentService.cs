using Api.Persistance.Model;

namespace Api.Services.Interfaces;

public interface IFinancialDocumentService
{
    FinancialDocument? GetFinancialDocument(Guid DocumentId);
}
