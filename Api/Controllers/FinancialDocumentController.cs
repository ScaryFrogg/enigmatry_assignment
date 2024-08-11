using Api.Dto;
using Api.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialDocumentController : ControllerBase
{
    private readonly DatabaseContext _context;

    private readonly ILogger<FinancialDocumentController> _logger;

    public FinancialDocumentController(DatabaseContext context, ILogger<FinancialDocumentController> logger)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetFinancialDocument")]
    public IActionResult Get([FromBody] GetFinancialDocumentRequest request)
    {

        return Ok(new { Message = "Request is valid." });
    }
}
