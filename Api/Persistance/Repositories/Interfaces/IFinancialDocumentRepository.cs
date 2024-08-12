using Api.Persistance.Model;

namespace Api.Persistance.Repositories.Interfaces;

public interface IFinancialDocumentRepository : IRepository<FinancialDocument>
{
    FinancialDocument? GetDocumentWithClientData(Guid documentId);
}
