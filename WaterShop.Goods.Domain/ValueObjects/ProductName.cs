namespace WaterShop.Goods.Domain.ValueObjects;

public class ProductName
{
    private ProductName(string name)
    {
        Value = name;
    }

    public string Value { get; init; }

    public static ProductName Create(string productName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName, "The product name can`t be empty");
        return new ProductName(productName);
    }
}
