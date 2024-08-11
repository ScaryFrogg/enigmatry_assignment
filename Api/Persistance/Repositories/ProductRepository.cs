using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;

namespace Api.Persistance.Repositories;

public class ProductRepository(DatabaseContext context) : BaseRepository<Product>(context), IProductRepository
{

    public Product? GetByProductCode(string productCode)
    {
        if (string.IsNullOrEmpty(productCode))
        {
            return null;
        }
        return _context.Products.FirstOrDefault(p => p.ProductCode == productCode);
    }
}
