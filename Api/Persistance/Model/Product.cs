using System.Text.RegularExpressions;

namespace Api.Persistance.Model;

public class Product : IProduct
{
    public string ProductCode { get; }
    public string[] KeysToMask { get; }
    protected string MaskedField => "#####";

    public Product(string productCode, string[] keysToMask)
    {
        ProductCode = productCode;
        KeysToMask = keysToMask;
    }

    public string Mask(string input)
    {
        foreach (string key in KeysToMask)
        {
            string pattern = $@"\""{key}\""\s*?:.+?,";
            string replacement = $@"""{key}"": ""{MaskedField}"",";
            input = Regex.Replace(input, pattern, replacement);
        }
        return input;
    }
}

public interface IProduct
{
    string Mask(string input);
}

public class ProductStrategy
{
    private readonly Dictionary<string, IProduct> _products;
    private static readonly ProductStrategy _instance = new ProductStrategy();
    public static ProductStrategy Instance => _instance;
    public static readonly string PRODUCT_CODE_A = "ProductA";
    public static readonly string PRODUCT_CODE_B = "ProductB";
    private ProductStrategy()
    {
        _products = new Dictionary<string, IProduct>
        {
            { PRODUCT_CODE_A, new Product(PRODUCT_CODE_A, ["transaction_id", "description"]) },
            { PRODUCT_CODE_B, new Product(PRODUCT_CODE_B, ["transaction_id"]) }
        };
    }

    public IProduct? GetProduct(string? productCode)
    {
        if (string.IsNullOrEmpty(productCode))
        {
            return null;
        }
        if (_products.TryGetValue(productCode, out var product))
        {
            return product;
        }
        return null;
    }
}