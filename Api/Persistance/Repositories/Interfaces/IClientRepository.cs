using Api.Dto;
using Api.Persistance.Model;

namespace Api.Persistance.Repositories.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    Company GetCompanyInfo(long vatNumber);
    ClientWhitelisting? GetClientWhitelisting(Guid clientId, Guid tenantId);
}
