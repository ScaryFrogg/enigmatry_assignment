using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.Domain.Products;

public class Product : IProduct
{
    public string ProductCode { get; }
    public string[] KeysToMask { get; }
    public HashSet<string> KeysToHash { get; }
    protected string MaskedField => "#####";

    public Product(string productCode, string[] keysToMask, HashSet<string> keysToHash)
    {
        ProductCode = productCode;
        KeysToMask = keysToMask;
        KeysToHash = keysToHash;
    }

    public string Anonymize(string input)
    {
        string keysToMatch = string.Join("|", KeysToHash.Union(KeysToMask));
        string pattern = $@"\""({keysToMatch})\""\s*?:(.+?,)";
        return Regex.Replace(input, pattern, match =>
        {
            string key = match.Groups[1].Value;
            string value = match.Groups[2].Value;

            if (KeysToHash.Contains(key))
            {
                string hashedValue = HashValue(value.Trim('"'));
                value = $"\"{hashedValue}\"";
            }
            else
            {
                value = MaskedField;
            }

            return $@"""{key}"": {value},";
        });
    }

    static string HashValue(string value)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}