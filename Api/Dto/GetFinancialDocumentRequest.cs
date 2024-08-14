
namespace Api.Dto;

public class GetFinancialDocumentRequest
{
    public string? ProductCode { get; set; }
    public Guid TenantId { get; set; }
    public Guid DocumentId { get; set; }
}
