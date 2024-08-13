using Api.Dto;
using Api.Persistance.Model;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialDocumentController : ControllerBase
{
    private readonly ILogger<FinancialDocumentController> _logger;
    private readonly IFinancialDocumentService _financialDocumentService;
    // private readonly ITenantService _tenantService;
    // private readonly IClientService _clientService;

    public FinancialDocumentController(
        ILogger<FinancialDocumentController> logger,
        IFinancialDocumentService financialDocumentService
    // , ITenantService tenantService, IClientService clientService
    )
    {
        _logger = logger;
        _financialDocumentService = financialDocumentService;
        // _tenantService = tenantService;
        // _clientService = clientService;
    }

    [HttpGet(Name = "GetFinancialDocument")]
    public IActionResult Get([FromBody] GetFinancialDocumentRequest request)
    {
        _logger.LogDebug($"GetFinancialDocumentRequest [DocumentId]${request.DocumentId} [ProductCode]${request.ProductCode} [TenantId]${request.TenantId}");
        IProduct? productType = ProductStrategy.Instance.GetProduct(request.ProductCode);
        if (productType == null)
        {
            return StatusCode(403, $"ProductCode:${request.ProductCode}  is unsupported");
        }

        var doc = _financialDocumentService.GetFinancialDocument(request.DocumentId);
        if (doc == null)
        {
            return StatusCode(403, "Product is not supported.");
        }

        //TODO VP productType.Mask("asd");
        return Ok(new { Message = "Request is valid." });
    }
}