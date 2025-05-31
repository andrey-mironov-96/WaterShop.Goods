using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Domain.Mappers;

public class ProductMapper
{
    public static ProductDTO? ToDTO(Product? product)
    {
        if (product is null) return null;
        return new ProductDTO()
        {
            Identity = product.Identity,
            Name = product.Name.Value,
            CreateAt = product.Batch.CreateAt,
            BatchNumber = product.Batch.Value,
            Brand = product.Brand.Value
        };
    }

    public static IEnumerable<ProductDTO> ToDTO(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            yield return ToDTO(product)!;
        }
    }
}
