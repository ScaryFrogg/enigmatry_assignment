using Api.Persistance.Model;

namespace Api.Persistance.Repositories.Interfaces;

public interface IFinancialDocumentRepository : IRepository<FinancialDocument>
{
    //Returns ClientId and ClientVatNumber
    (Guid, long)? GetClientInfo(Guid documentId);
    FinancialDocument? GetDocumentWithClientData(Guid documentId);
}
