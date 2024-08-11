
namespace Api.Dto;

public class GetFinancialDocumentResponse
{
    public string ProductCode { get; set; }
    public Guid TenantId { get; set; }
    public Guid DocumentId { get; set; }
}
