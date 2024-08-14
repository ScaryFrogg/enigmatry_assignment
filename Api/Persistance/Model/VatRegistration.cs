using System.ComponentModel.DataAnnotations;
using Api.Persistance.Model.Enums;

namespace Api.Persistance.Model;

public class VatRegistration
{
    [Key]
    public long VatNumber { get; set; }
    public required string RegistrationNumber { get; set; }
    public CompanyType CompanyType { get; set; } = CompanyType.Small;

    public IList<FinancialDocument> FinancialDocuments { get; set; } = new List<FinancialDocument>();
    public Client? Client { get; set; }
}
