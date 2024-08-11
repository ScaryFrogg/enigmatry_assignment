using Api.Persistance.Repositories.Interfaces;

namespace Api.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool IsProductSupported(string productCode)
    {
        var product = _productRepository.GetByProductCode(productCode);

        return product?.IsSupported ?? false;
    }
}
