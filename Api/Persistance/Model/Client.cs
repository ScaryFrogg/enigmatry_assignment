using System.ComponentModel.DataAnnotations;

namespace Api.Persistance.Model;

public class Client
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public ISet<VatRegistration> ClientVats { get; set; } = new HashSet<VatRegistration>();
    public List<ClientWhitelisting> ClientWhitelisting { get; set; } = new List<ClientWhitelisting>();
}