namespace WaterShop.Goods.Domain.Primitives.Filters;

public sealed class ProductFilter : Filter
{
    public static class BatchFilterLabels
    {
        public const string BatchValue = "batch_value";
        public const string BatchCreatedFromOrEqual = "batch_created_fromOrEqual";
        public const string BatchCreatedToOrEqual = "batch_created_toOrEqual";
        public const string BatchCreatedEqual = "batch_created_equal";
    }

    public static class TypeFilterLabels
    {
        public const string TypeValue = "type_value";
    }

    public static class BrandFilterLabels
    {
        public const string ProductBrand = "product_brand";
    }

    public static class ProductFilterLabels
    {
        public const string ProductName = "product_name";
    }

    public ProductFilter()
    {
        Filters = new();
    }

    public override List<FilterItem> Filters { get; set; }
}