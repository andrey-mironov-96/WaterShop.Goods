using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Application.Repositories;

public interface IProductRepository
{
    public Task<PageableData<ProductDto>> GetProducts(PageableData<ProductDto> pData);
}
