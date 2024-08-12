using Api.Dto;
using Api.Persistance.Model;
using Api.Persistance.Model.Enums;
using Microsoft.AspNetCore.Mvc;
namespace ApiTest;

[TestFixture]
public class FinancialDocumentsTests : TestBase
{
    private Guid _docId;

    [TestCase("ProductX")]
    public void TestHappyFlow(string productCode)
    {
        //Arrange
        Client client = new Client()
        {
            ClinetVat = "Client1VAT",
            CompanyType = CompanyType.Medium
        };
        Tenant tennant = new Tenant();
        FinancialDocument doc1 = new FinancialDocument()
        {
            Client = client,
            Tenant = tennant,
        };

        _docId = doc1.Id;
        _financialDocumentRepository.AddAsync(doc1);

        // Act
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = productCode,
            DocumentId = _docId,
            TenantId = Guid.NewGuid(),
        };
        IActionResult result = _controller.Get(request);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [TestCase(false, true, typeof(ForbidResult))]
    [TestCase(true, false, typeof(ForbidResult))]
    [TestCase(true, true, typeof(OkResult))]
    public void TestWhiteListings(bool isTennantWhitelisted, bool isClientWhitelisted, Type expectedResult)
    {
        //Arrange
        Client client = new Client()
        {
            ClinetVat = "Client1VAT",
            IsWhitelisted = isClientWhitelisted,
            CompanyType = CompanyType.Medium
        };
        Tenant tennant = new Tenant()
        {
            IsWhitelisted = isTennantWhitelisted,
        };
        FinancialDocument doc1 = new FinancialDocument()
        {
            Client = client,
            Tenant = tennant,
        };

        _docId = doc1.Id;
        _financialDocumentRepository.AddAsync(doc1);
        // Act
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = "x",
            DocumentId = _docId,
            TenantId = Guid.NewGuid(),
        };
        IActionResult result = _controller.Get(request);

        Assert.IsInstanceOf(expectedResult, result);
    }
}