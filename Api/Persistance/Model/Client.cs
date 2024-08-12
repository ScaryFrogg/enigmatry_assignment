using System.ComponentModel.DataAnnotations;
using Api.Persistance.Model.Enums;

namespace Api.Persistance.Model;

public class Client
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string ClinetVat { get; set; }
    public bool IsWhitelisted { get; set; } = true;
    public  CompanyType CompanyType { get; set; } = CompanyType.Small;
}
