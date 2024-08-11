using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class Client
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
