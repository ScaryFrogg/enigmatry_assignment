using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class FinancialDocument
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Tenant Tenant { get; set; }
    public required Client Client { get; set; }
}
