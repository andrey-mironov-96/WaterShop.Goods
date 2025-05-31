using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Domain.Entities;

public class ProductType : BaseEntity
{
    public required string Value { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
