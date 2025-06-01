namespace WaterShop.Goods.Domain.Primitives.Filters;

public sealed class FilterItem
{
    public required string Value { get; set; }

    public required string Label { get; set; }
}