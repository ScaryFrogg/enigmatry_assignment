using Api.Persistance.Model;
using Api.Persistance.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IFinancialDocumentRepository _financialDocumentRepository;

    public ProductService(IProductRepository productRepository, IFinancialDocumentRepository financialDocumentRepository = null)
    {
        _productRepository = productRepository;
        _financialDocumentRepository = financialDocumentRepository;
    }

    public FinancialDocument? GetFinancialDocument(Guid DocumentId)
    {
        return _financialDocumentRepository.GetDocumentWithClientData(DocumentId);
    }

    public bool IsProductSupported(string productCode)
    {
        var product = _productRepository.GetByProductCode(productCode);

        return product?.IsSupported ?? false;
    }
}
