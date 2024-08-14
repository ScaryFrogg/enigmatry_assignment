using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class ClientWhitelisting
{
    [Key]
    public long Id { get; set; }
    public required Client Client { get; set; }
    public required Tenant Tenant { get; set; }
}