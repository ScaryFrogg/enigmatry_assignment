using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class TenantRepository(DatabaseContext context) : BaseRepository<Tenant>(context), ITenantRepository
{
    public void WhitelistClient(Tenant tenant, Client client)
    {
        var whitelisting = new ClientWhitelisting()
        {
            Tenant = tenant,
            Client = client,
        };
        
        tenant.ClientWhitelisting.Add(whitelisting);
        client.ClientWhitelisting.Add(whitelisting);
        _context.ClientWhitelistings.Add(whitelisting);
    }
}
