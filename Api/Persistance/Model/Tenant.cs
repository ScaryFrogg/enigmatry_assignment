using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class Tenant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}