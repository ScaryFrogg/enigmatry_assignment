using Api.Dto;
using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class ClientRepository(DatabaseContext context) : BaseRepository<Client>(context), IClientRepository
{
    public Company GetCompanyInfo(long vatNumber)
    {
        return _context.VatRegistrations
        .Where(v => v.VatNumber == vatNumber)
        .Select(v => new Company
        {
            CompanyType = v.CompanyType,
            RegistrationNumber = v.RegistrationNumber
        })
        .First();
    }

    public ClientWhitelisting? GetClientWhitelisting(Guid clientId, Guid tenantId)
    {
        return _context.ClientWhitelistings
        .Where(w => w.Client.Id == clientId && w.Tenant.Id == tenantId)
        .SingleOrDefault();
    }
}
