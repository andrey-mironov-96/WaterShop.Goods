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

    [Fact]
    public async Task ShouldBeReturnProductsCreatedToday()
    {
        DateTime today = DateTime.Now;
        const ushort page = 1;
        const ushort pageSize = 1000;
        PageableData<ProductDto> pageableData = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = [
                    new(){ Label = ProductFilter.BatchFilterLabels.BatchCreatedEqual, Value = today.ToString("dd.MM.yyyy")},
                   
                ]
            }
        };

        GetProductsQuery query = new(pageableData);
        var responsePageableData = await this.Mediator.Send(query);

        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.True(responsePageableData.IsSuccess);
        Assert.Equal(page, responsePageableData.Value.Page);
        Assert.Equal(pageSize, responsePageableData.Value.PageSize);
        Assert.NotEqual(0u, responsePageableData.Value.Total);
        Assert.NotEmpty(responsePageableData.Value.Data);
        Assert.Contains(responsePageableData.Value.Data, value => value.CreateAt.Date == today.Date);
    }

    [Fact]
    public async Task ShouldBeReturnProductsOfBatchNumberEqual_Batch_10()
    {
        const ushort page = 1;
        const ushort pageSize = 1000;
        const string batchNumber = "Batch_10";

        PageableData<ProductDto> pageableData = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = [
                    new(){ Label = ProductFilter.BatchFilterLabels.BatchValue, Value = batchNumber},
                ]
            }
        };

        GetProductsQuery query = new(pageableData);
        var responsePageableData = await this.Mediator.Send(query);

        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.True(responsePageableData.IsSuccess);
        Assert.Equal(page, responsePageableData.Value.Page);
        Assert.Equal(pageSize, responsePageableData.Value.PageSize);
        Assert.NotEqual(0u, responsePageableData.Value.Total);
        Assert.NotEmpty(responsePageableData.Value.Data);
        Assert.Contains(responsePageableData.Value.Data, value => value.BatchNumber == batchNumber);
    }

    [Fact]
    public async Task ShouldBeReturnProductsWithBrand_Dobry()
    {
        const ushort page = 1;
        const ushort pageSize = 1000;
        const string brandName = "Добрый";

        PageableData<ProductDto> pageableData = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = [
                   new(){ Label = ProductFilter.BrandFilterLabels.ProductBrand, Value = brandName},
                ]
            }
        };

        GetProductsQuery query = new(pageableData);
        var responsePageableData = await this.Mediator.Send(query);

        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.True(responsePageableData.IsSuccess);
        Assert.Equal(page, responsePageableData.Value.Page);
        Assert.Equal(pageSize, responsePageableData.Value.PageSize);
        Assert.NotEqual(0u, responsePageableData.Value.Total);
        Assert.NotEmpty(responsePageableData.Value.Data);
        Assert.Contains(responsePageableData.Value.Data, value => value.Brand == brandName);
    }

    [Fact]
    public async Task ShouldBeReturnProductsWithName_name_5()
    {
        const ushort page = 1;
        const ushort pageSize = 1000;
        const string productName = "name_5";
        const uint totalCount = 1;

        PageableData<ProductDto> pageableData = new(page, pageSize)
        {
            Filter = new ProductFilter()
            {
                Filters = [
                   new(){ Label = ProductFilter.ProductFilterLabels.ProductName, Value = productName},
                ]
            }
        };

        GetProductsQuery query = new(pageableData);
        var responsePageableData = await this.Mediator.Send(query);

        Assert.NotNull(responsePageableData);
        Assert.NotNull(responsePageableData.Value);
        Assert.True(responsePageableData.IsSuccess);
        Assert.Equal(page, responsePageableData.Value.Page);
        Assert.Equal(pageSize, responsePageableData.Value.PageSize);
        Assert.NotEqual(0u, responsePageableData.Value.Total);
        Assert.NotEmpty(responsePageableData.Value.Data);
        Assert.Equal(totalCount, responsePageableData.Value.Total);
        Assert.Equal(responsePageableData.Value.Total, (uint)responsePageableData.Value.Data.Count());
        Assert.Contains(responsePageableData.Value.Data, value => value.Name.EndsWith(productName));
    }

}