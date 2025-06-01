namespace WaterShop.Goods.Domain.Primitives.Filters;

public sealed class ProductFilter : Filter
{
    public ProductFilter()
    {
        Filters = new();
    }

    public override List<FilterItem> Filters { get; set; }
}