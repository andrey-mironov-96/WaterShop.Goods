using WaterShop.Goods.Domain.Utils.Extensions;
using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using WaterShop.Goods.Application.Repositories;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Entities;
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
        public async Task<PageableData<ProductDto>> GetProducts(PageableData<ProductDto> pData)
        {
            var query = _dbContext.Products
                .Include(p => p.Batch)
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .AsNoTracking();

            AddFilters(ref query, pData);

            pData.Total = (uint)query.Count();

            pData.Data = await query
                .Skip(pData.GetSkipped())
                .Take(pData.PageSize)
                .Select(product => ProductMapper.ToDto(product)!).ToListAsync();

            var x = _dbContext.ProductBatches.GroupBy(x => x.CreateAt).Select(x => x.Key);
            return pData;
        }

        private static void AddFilters(ref IQueryable<Product> query, PageableData<ProductDto> pageableData)
        {
            if (pageableData.Filter != null)
            {
                query = pageableData.Filter.Filters.Aggregate(query, (current, filter) => filter.Label switch
                {
                    "batch_value" => current.Where(product => product.Batch.Value == filter.Value),
                    "batch_created_fromOrEqual" => BatchCreatedFromOrEqual(current, filter.Value),
                    "batch_created_toOrEqual" => BatchCreatedToOrEqual(current, filter.Value),

                    "type_value" => current.Where(product => product.Type.Value == filter.Value),

                    "product_name" => current.Where(product => EF.Functions.ILike(product.Name.Value, $"%{filter.Value}%")),

                    "product_brand" => current.Where(product => product.Brand.Value == filter.Value),
                    _ => throw new ArgumentException("Unknow filter")
                });
            }
        }

        private static IQueryable<Product> BatchCreatedFromOrEqual(IQueryable<Product> query, string value)
        {
            DateTime date = DateTimeExtensions.Parse(value);
            query = query.Where(product => product.Batch.CreateAt.Date >= date);
            return query;
        }

        private static IQueryable<Product> BatchCreatedToOrEqual(IQueryable<Product> query, string value)
        {
            DateTime date = DateTimeExtensions.Parse(value);
            query = query.Where(product => date >= product.Batch.CreateAt.Date);
            return query;
        }
    }
}
