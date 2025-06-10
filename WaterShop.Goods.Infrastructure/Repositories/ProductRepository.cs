using WaterShop.Goods.Domain.Utils.Extensions;
using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using WaterShop.Goods.Application.Repositories;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Entities;
using WaterShop.Goods.Domain.Mappers;
using WaterShop.Goods.Domain.Primitives;
using WaterShop.Goods.Infrastructure.Context;
using WaterShop.Goods.Domain.Primitives.Filters;

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

            return pData;
        }

        private static void AddFilters(ref IQueryable<Product> query, PageableData<ProductDto> pageableData)
        {
            if (pageableData.Filter != null)
            {
                
                query = pageableData.Filter.Filters.Aggregate(query, (current, filter) => filter.Label switch
                {
                    ProductFilter.BatchFilterLabels.BatchValue => current.Where(product => product.Batch.Value == filter.Value),
                    ProductFilter.BatchFilterLabels.BatchCreatedFromOrEqual => BatchCreatedFromOrEqual(current, filter.Value),
                    ProductFilter.BatchFilterLabels.BatchCreatedToOrEqual => BatchCreatedToOrEqual(current, filter.Value),
                    ProductFilter.BatchFilterLabels.BatchCreatedEqual => BatchCreatedEqual(current, filter.Value),
                    ProductFilter.TypeFilterLabels.TypeValue => current.Where(product => product.Type.Value == filter.Value),
                    ProductFilter.ProductFilterLabels.ProductName => current.Where(product =>filter.Value == product.Name.Value),
                    ProductFilter.BrandFilterLabels.ProductBrand => current.Where(product => product.Brand.Value == filter.Value),

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

        private static IQueryable<Product> BatchCreatedEqual(IQueryable<Product> query, string value)
        {
            DateTime date = DateTimeExtensions.Parse(value);
            query = query.Where(product => date == product.Batch.CreateAt.Date);
            return query;
        }
    }
}
