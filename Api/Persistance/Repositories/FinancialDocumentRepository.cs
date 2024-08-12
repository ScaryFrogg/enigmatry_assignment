using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class FinancialDocumentRepository(DatabaseContext context) : BaseRepository<FinancialDocument>(context), IFinancialDocumentRepository
{
    public FinancialDocument? GetDocumentWithClientData(Guid documentId)
    {
        return _dbSet.FirstOrDefault(d => d.Id == documentId);
    }
}