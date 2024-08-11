using Api.Persistance.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services;

public class ProductService: IProductService
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
