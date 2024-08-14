using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class Tenant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsWhitelisted { get; set; } = true;

    public IList<FinancialDocument> FinancialDocuments { get; set; } = new List<FinancialDocument>();
    public List<ClientWhitelisting> ClientWhitelisting { get; set; } = new List<ClientWhitelisting>();
}