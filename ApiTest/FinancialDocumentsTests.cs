using System.Reflection.Metadata;
using Api.Controllers;
using Api.Dto;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
namespace ApiTest;

[TestFixture]
public class FinancialDocumentsTests
{
    private Mock<IProductService> _mockProductService;
    // private Mock<ITenantService> _mockTenantService;
    // private Mock<IClientService> _mockClientService;
    private Mock<ILogger<FinancialDocumentController>> _mockLogger;
    private FinancialDocumentController _controller;

    [SetUp]
    public void Setup()
    {
        _mockProductService = new Mock<IProductService>();
        // _mockTenantService = new Mock<ITenantService>();
        // _mockClientService = new Mock<IClientService>();
        _mockLogger = new Mock<ILogger<FinancialDocumentController>>();

        _controller = new FinancialDocumentController(
            _mockLogger.Object,
            _mockProductService.Object
        //     _mockTenantService.Object,
        //     _mockClientService.Object,
            );
    }

    [TestCase("ProductX")]
    public void TestProductCode(string productCode)
    {
        //Arrange
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = productCode,
            DocumentId = Guid.NewGuid(),
            TenantId = Guid.NewGuid(),
        };
        // Act
        IActionResult result = _controller.Get(request);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}