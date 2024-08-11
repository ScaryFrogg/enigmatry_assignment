using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class TenantRepository(DatabaseContext context) : BaseRepository<Tenant>(context), ITenantRepository
{

}
