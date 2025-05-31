using WaterShop.Goods.Application.Feature.Products.Queries;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Errors;
using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Test;

public class GetProductQueryHandlerTest : BaseTest
{
    public GetProductQueryHandlerTest()
    {
        this.RegisterApplicationHandlers();

    }

    [Fact]
    public async Task ShouldBeReturnProduct()
    {
        const ushort page = 1;
        const ushort pageSize = 10;
        PageableData<ProductDTO> pDataRequest = new(page, pageSize);
        GetProductsQuery query = new(pDataRequest); 
        var pDataResponse = await this.Mediator.Send(query);

        Assert.NotNull(pDataResponse);
        Assert.NotNull(pDataResponse.Value);
        Assert.NotEmpty(pDataResponse.Value.Data);
        Assert.NotEqual(0u, pDataResponse.Value.Total);
        Assert.Equal(page, pDataResponse.Value.Page);
        Assert.Equal(pageSize, pDataResponse.Value.PageSize);

    }

    [Fact]
    public async Task ShouldBeCriticalError_WhenThrowNullReferenceException()
    {

        PageableData<ProductDTO>? pDataRequest = null;
        GetProductsQuery query = new(pDataRequest!);
        var pDataResponse = await this.Mediator.Send(query);
        Assert.NotNull(pDataResponse);
        Assert.Null(pDataResponse.Value);
        Assert.Equal(new CriticalError(), pDataResponse.Error);

    }
}