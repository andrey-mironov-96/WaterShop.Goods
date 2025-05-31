using WaterShop.Goods.Domain.Primitives;
using WaterShop.Goods.Domain.ValueObjects;

namespace WaterShop.Goods.Domain.Entities;

public sealed class Product : BaseEntity
{
    public required ProductName Name { get; init; }

    public required ProductBatch Batch { get; set; }

    public required ProductBrand Brand { get; set; }

    public required ProductType Type { get; set; }

    public Guid IdentityBatch { get; set; }
    public Guid IdentityBrand { get; set; }
    public Guid IdentityType { get; set; }


}
