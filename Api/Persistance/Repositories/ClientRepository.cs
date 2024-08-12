using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class ClientRepository(DatabaseContext context) : BaseRepository<Client>(context), IClientRepository
{
    public void AddDocument(FinancialDocument document)
    {
        _context.Add<FinancialDocument>(document);
    }

}
