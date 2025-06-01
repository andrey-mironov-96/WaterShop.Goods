using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Domain.DTO;

public class ProductDto : BaseDto
{
    public required string Name { get; set; }
    public required string Brand { get; set; }

    public required DateTime CreateAt { get; set; }
    public required string BatchNumber { get; set; }

    public required string Type { get; set; }
}
