using Api.Persistance.Model;

namespace Api.Persistance.Repositories.Interfaces;

public interface ITenantRepository : IRepository<Tenant>
{
    void WhitelistClient(Tenant tenant, Client client);
}
