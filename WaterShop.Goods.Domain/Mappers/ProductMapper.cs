using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Domain.Mappers;

public static class ProductMapper
{
    public static ProductDto? ToDto(Product? product)
    {
        if (product is null) return null;
        return new ProductDto()
        {
            Identity = product.Identity,
            Name = product.Name.Value,
            CreateAt = product.Batch.CreateAt,
            BatchNumber = product.Batch.Value,
            Brand = product.Brand.Value,
            Type = product.Type.Value,
        };
    }

    public static IEnumerable<ProductDto> ToDto(IEnumerable<Product> products)
    {
        return products.Select(product => ToDto(product)!);
    }
}
