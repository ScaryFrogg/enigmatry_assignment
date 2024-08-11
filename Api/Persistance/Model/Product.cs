using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ProductCode { get; set; }
    public bool IsSupported { get; set; }
}
