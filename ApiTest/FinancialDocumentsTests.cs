using Api.Domain.Products;
using Api.Dto;
using Api.Persistance.Model;
using Api.Persistance.Model.Enums;
using Microsoft.AspNetCore.Mvc;
namespace ApiTest;

[TestFixture]
public class FinancialDocumentsTests : TestBase
{
    [Test]
    public void TestHappyFlow()
    {
        //Arrange
        (Tenant, FinancialDocument) data = InsertData();

        // Act
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = ProductStrategy.PRODUCT_CODE_A,
            TenantId = data.Item1.Id,
            DocumentId = data.Item2.Id,
        };
        IActionResult result = _controller.Get(request);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        ObjectResult objectResult = result as ObjectResult;
        GetFinancialDocumentResponse responseData = objectResult.Value as GetFinancialDocumentResponse;
        Assert.That(objectResult?.StatusCode, Is.EqualTo(200));
    }

    [TestCase(false, true, ProductStrategy.PRODUCT_CODE_A, 403)]
    [TestCase(true, false, ProductStrategy.PRODUCT_CODE_A, 403)]
    [TestCase(true, true, "BAD PRODUCT CODE", 403)]
    [TestCase(true, true, ProductStrategy.PRODUCT_CODE_A, 200)]
    public void TestRequestValidation(bool isTennantWhitelisted, bool isClientWhitelisted, string productCode, int statusCode)
    {
        //Arrange
        (Tenant, FinancialDocument) data = InsertData(isTennantWhitelisted, isClientWhitelisted);
        // Act
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = productCode,
            TenantId = data.Item1.Id,
            DocumentId = data.Item2.Id,
        };
        IActionResult result = _controller.Get(request);
        Assert.IsInstanceOf<ObjectResult>(result);
        ObjectResult objectResult = result as ObjectResult;
        Assert.AreEqual(statusCode, objectResult?.StatusCode);
    }

    [TestCase(CompanyType.Small, 403)]
    [TestCase(CompanyType.Medium, 200)]
    [TestCase(CompanyType.Large, 200)]
    public void TestCompanySizeValidation(CompanyType companyType, int statusCode)
    {
        //Arrange
        (Tenant, FinancialDocument) data = InsertData(companyType: companyType);
        // Act
        GetFinancialDocumentRequest request = new GetFinancialDocumentRequest()
        {
            ProductCode = ProductStrategy.PRODUCT_CODE_B,
            TenantId = data.Item1.Id,
            DocumentId = data.Item2.Id,
        };
        IActionResult result = _controller.Get(request);
        Assert.IsInstanceOf<ObjectResult>(result);
        ObjectResult objectResult = result as ObjectResult;
        Assert.AreEqual(statusCode, objectResult?.StatusCode);
    }
}