using Api.Persistance.Model;

namespace Api.Persistance.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Product? GetByProductCode(string productCode);
}
