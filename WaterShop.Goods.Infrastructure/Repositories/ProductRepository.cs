using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using WaterShop.Goods.Application.Repositories;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Mappers;
using WaterShop.Goods.Domain.Primitives;
using WaterShop.Goods.Infrastructure.Context;

[assembly: InternalsVisibleTo("WaterShop.Goods.Test")]
namespace WaterShop.Goods.Infrastructure.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PageableData<ProductDTO>> GetProducts(PageableData<ProductDTO> pData)
        {
            var query = _dbContext.Products.AsNoTracking()
                .Include(p => p.Batch)
                .Include(p => p.Brand)
                .Include(p => p.Type);

            pData.Total = (uint)query.Count();

            pData.Data = await query
                .Skip(pData.GetSkipped())
                .Take(pData.PageSize)
                .Select(product => ProductMapper.ToDTO(product)!).ToListAsync();

            return pData;
        }
    }
}
