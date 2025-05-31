using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Domain.DTO;

public class ProductDTO : BaseDTO
{
    public required string Name { get; set; }
    public required string Brand { get; set; }

    public required DateTime CreateAt { get; set; }
    public required string BatchNumber { get; set; }
}
