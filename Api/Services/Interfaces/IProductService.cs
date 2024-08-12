using Api.Persistance.Model;

namespace Api.Services.Interfaces;

public interface IProductService
{
    bool IsProductSupported(string productCode);
    FinancialDocument? GetFinancialDocument(Guid DocumentId);
}
