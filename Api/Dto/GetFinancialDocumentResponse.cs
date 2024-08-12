
using Api.Persistance.Model.Enums;

namespace Api.Dto;

public class GetFinancialDocumentResponse
{
    public required string Data { get; set; }
    public required string Company { get; set; }
}
public class Company
{
    public required string RegistrationNumber { get; set; }
    public required CompanyType CompanyType { get; set; }
}
