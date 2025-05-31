using WaterShop.Goods.Application.Utils;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Application.Feature.Products.Queries;

public record GetProductsQuery(PageableData<ProductDTO> PageableData) : IQuery<Result<PageableData<ProductDTO>>>;
