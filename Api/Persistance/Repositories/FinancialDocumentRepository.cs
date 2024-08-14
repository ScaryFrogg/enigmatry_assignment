using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class FinancialDocumentRepository(DatabaseContext context) : BaseRepository<FinancialDocument>(context), IFinancialDocumentRepository
{
    public (Guid, long)? GetClientInfo(Guid documentId)
    {
        var result = _dbSet
                    .Where(doc => doc.Id == documentId)
                    .Select(doc => new
                    {
                        ClientId = _context.Clients
                            .Where(c => c.ClientVats.Contains(doc.ClientVat))
                            .Select(c => c.Id)
                            .FirstOrDefault(),
                        VatNumber = doc.ClientVat.VatNumber
                    })
                    .FirstOrDefault();

        if (result == null)
        {
            return null;
        }

        return (result.ClientId, result.VatNumber);
    }

    public FinancialDocument? GetDocumentWithClientData(Guid documentId)
    {
        return _dbSet.FirstOrDefault(d => d.Id == documentId);
    }
}