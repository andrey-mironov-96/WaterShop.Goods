using WaterShop.Goods.Application.Feature.Products.Queries;
using WaterShop.Goods.Domain.DTO;
using WaterShop.Goods.Domain.Errors;
using WaterShop.Goods.Domain.Primitives;
using WaterShop.Goods.Domain.Primitives.Filters;
using WaterShop.Goods.Domain.ValueObjects;

namespace WaterShop.Goods.Test;

public class GetProductQueryHandlerTest : BaseTest
{
    public GetProductQueryHandlerTest()
    {
        this.RegisterApplicationHandlers();

    }

    [Fact]
    public async Task ShouldBeReturnProducts()
    {
        const ushort page = 1;
        const ushort pageSize = 10;
        PageableData<ProductDto> pDataRequest = new(page, pageSize);
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

        PageableData<ProductDto>? pDataRequest = null;
        GetProductsQuery query = new(pDataRequest!);
        var pDataResponse = await this.Mediator.Send(query);
        Assert.NotNull(pDataResponse);
        Assert.Null(pDataResponse.Value);
        Assert.Equal(new CriticalError(), pDataResponse.Error);
    }

    [Fact]
    public async Task ShouldBeReturnProductsWithTypeCoke()
    {
        const ushort page = 1;
        const ushort pageSize = 10;
        const string type = "Coke";
        PageableData<ProductDto> pDataRequest = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = new List<FilterItem>()
                {
                    new(){Label = "type_value", Value = type}
                }
            }
        };
        GetProductsQuery query = new(pDataRequest);
        var responsePageableData = await this.Mediator.Send(query);
        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.Contains(responsePageableData.Value.Data, x => x.Type == type);
    }

    [Fact]
    public async Task ShouldBeReturnProductsWithCreateFromOrEqualYesterday()
    {
        const ushort page = 1;
        const ushort pageSize = 1000;
        DateTime date = DateTime.Now.AddDays(-1);
        string dateAsString = date.ToString("dd.MM.yyyy");
        PageableData<ProductDto> pDataRequest = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = new List<FilterItem>()
                {
                    new(){ Label = "batch_created_fromOrEqual", Value = dateAsString}
                }
            }
        };
        GetProductsQuery query = new(pDataRequest);
        var responsePageableData = await this.Mediator.Send(query);
        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.Contains(responsePageableData.Value.Data, x => x.CreateAt >= date);
    }


    [Fact]
    public async Task ShouldBeReturnProductsWithCreateToOrEqualYesterday()
    {
        const ushort page = 1;
        const ushort pageSize = 1000;
        DateTime date = DateTime.Now.AddDays(-1);
        string dateAsString = date.ToString("dd.MM.yyyy");
        PageableData<ProductDto> pDataRequest = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = new List<FilterItem>()
                {
                    new(){ Label = "batch_created_toOrEqual", Value = dateAsString}
                }
            }
        };
        GetProductsQuery query = new(pDataRequest);
        var responsePageableData = await this.Mediator.Send(query);
        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.Contains(responsePageableData.Value.Data, x => x.CreateAt <= date);
    }
    
}