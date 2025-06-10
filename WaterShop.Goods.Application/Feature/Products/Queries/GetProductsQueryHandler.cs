using Microsoft.Extensions.Logging;

using WaterShop.Goods.Application.Repositories;
using WaterShop.Goods.Application.Utils;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Errors;
using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Application.Feature.Products.Queries;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<PageableData<ProductDto>>>
{
    private readonly ILogger<GetProductsQueryHandler> _logger;
    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    public async Task<Result<PageableData<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetProducts(request.PageableData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when getting pageable data, {pData}", request.PageableData);
            return Result<PageableData<ProductDto>>.Failure(new CriticalError());
        }
    }
}
