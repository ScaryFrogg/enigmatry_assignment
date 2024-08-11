using Api.Dto;
using Api.Persistance;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialDocumentController : ControllerBase
{
    private readonly ILogger<FinancialDocumentController> _logger;
    private readonly IProductService _productService;
    // private readonly ITenantService _tenantService;
    // private readonly IClientService _clientService;

    public FinancialDocumentController(
        ILogger<FinancialDocumentController> logger,
        IProductService productService
    // , ITenantService tenantService, IClientService clientService
    )
    {
        _logger = logger;
        _productService = productService;
        // _tenantService = tenantService;
        // _clientService = clientService;
    }

    [HttpGet(Name = "GetFinancialDocument")]
    public IActionResult Get([FromBody] GetFinancialDocumentRequest request)
    {
        _logger.LogDebug($"GetFinancialDocumentRequest [DocumentId]${request.DocumentId} [ProductCode]${request.ProductCode} [TenantId]${request.TenantId}");
        bool isProductSupported = _productService.IsProductSupported(request.ProductCode);
        if (isProductSupported)
        {
            return StatusCode(403, "Product is not supported.");
        }
        return Ok(new { Message = "Request is valid." });
    }
}