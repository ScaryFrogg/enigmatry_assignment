using Api.Domain.Products;
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

    public FinancialDocumentController(
        ILogger<FinancialDocumentController> logger,
        IFinancialDocumentService financialDocumentService
    )
    {
        _logger = logger;
        _financialDocumentService = financialDocumentService;
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
        if (!_financialDocumentService.IsTennantWhitelisted(request.TenantId))
        {
            return StatusCode(403, $"Tennant tennantId:${request.TenantId} does not exist or is not whitelisted");
        }
        (Guid, long)? clientData = _financialDocumentService.GetClientId(request.DocumentId);
        if (clientData == null)
        {
            return StatusCode(403, $"ClientData not found for DocumentId {request.DocumentId}");
        }
        Guid clientId = clientData.Value.Item1;
        if (!_financialDocumentService.IsClientWhitelisted(request.TenantId, clientId))
        {
            return StatusCode(403, $"Tennant tennantId:${request.TenantId} is not whitelisted for Client clientId:${clientId}");
        }

        long vatNumber = clientData.Value.Item2;
        Company companyInfo = _financialDocumentService.GetCompanyInfo(vatNumber);

        if (companyInfo.CompanyType == Persistance.Model.Enums.CompanyType.Small)
        {
            return StatusCode(403, "Company is small");
        }
        GetFinancialDocumentResponse response = new GetFinancialDocumentResponse();

        FinancialDocument? doc = _financialDocumentService.GetFinancialDocument(request.DocumentId);
        if (doc == null)
        {
            return StatusCode(403, "Document not Found");
        }

        response.Company = companyInfo;
        response.Data = productType.Anonymize(doc.Data);
        return Ok(response);
    }
}