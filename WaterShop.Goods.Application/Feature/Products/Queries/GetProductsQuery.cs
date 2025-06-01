using WaterShop.Goods.Application.Utils;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Application.Feature.Products.Queries;

public record GetProductsQuery(PageableData<ProductDto> PageableData) : IQuery<Result<PageableData<ProductDto>>>;
