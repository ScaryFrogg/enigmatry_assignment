namespace Api.Domain.Products;

public class ProductStrategy
{
    private readonly Dictionary<string, IProduct> _products;
    private static readonly ProductStrategy _instance = new ProductStrategy();
    public static ProductStrategy Instance => _instance;
    public const string PRODUCT_CODE_A = "ProductA";
    public const string PRODUCT_CODE_B = "ProductB";
    private ProductStrategy()
    {
        _products = new Dictionary<string, IProduct>
        {
            { PRODUCT_CODE_A, new Product(PRODUCT_CODE_A, ["transaction_id", "description"],["account_number"]) },
            { PRODUCT_CODE_B, new Product(PRODUCT_CODE_B, ["description"],["transaction_id"]) }
        };
    }

    public IProduct? GetProduct(string? productCode)
    {
        if (string.IsNullOrEmpty(productCode))
        {
            return null;
        }
        if (_products.TryGetValue(productCode, out IProduct? product))
        {
            return product;
        }
        return null;
    }
}