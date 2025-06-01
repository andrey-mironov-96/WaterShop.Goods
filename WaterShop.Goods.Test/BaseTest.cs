using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using WaterShop.Goods.Application;
using WaterShop.Goods.Application.Repositories;
using WaterShop.Goods.Domain.Entities;
using WaterShop.Goods.Domain.ValueObjects;
using WaterShop.Goods.Infrastructure.Context;
using WaterShop.Goods.Infrastructure.Repositories;

namespace WaterShop.Goods.Test;

public abstract class BaseTest
{
    private IServiceCollection _services;
    private ServiceProvider? _serviceProvider;

    private IMediator? _mediator = null;

    protected BaseTest()
    {
        _services = new ServiceCollection();
        _services.AddLogging(conf => conf.AddConsole());
        RegisterAppDbContext();
    }

    protected ServiceProvider ServiceProvider
    {
        get
        {
            _serviceProvider = _services.BuildServiceProvider();
            return _serviceProvider;
        }
    }

    protected IMediator Mediator
    {
        get
        {
            _mediator ??= ServiceProvider.GetRequiredService<IMediator>();
            return _mediator;
        }
    }

    protected void RegisterApplicationHandlers()
    {
        _services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies((typeof(ApplicationAssemblyReference).Assembly)));

        _services.AddScoped<IProductRepository, ProductRepository>();
        
    }

    private void RegisterAppDbContext()
    {
        string dbName = "test_db_" + Guid.NewGuid();
        _services.AddDbContext<AppDbContext>(conf => conf.UseInMemoryDatabase(dbName));
        WarnUpDatabase();
    }
    private void WarnUpDatabase()
    {
        using AppDbContext database = ServiceProvider.GetRequiredService<AppDbContext>();
       
        List<ProductType> types = new()
        {
            new(){Value = "Soda"},
            new(){Value = "Water"},
            new(){Value = "Coke"}
        };

        List<ProductBrand> brands = new()
        {
            new(){Value = "Добрый"},
            new(){Value = "Черноголовка"},
            new(){Value = "Кока-кола"},
        };

        Random rand = new Random();

        List<Product> products = new();

        ProductBatch? batch = null;

        for (int index = 0; index < 100; index++)
        {

            ProductBrand brand = brands[rand.Next(0, 3)];
            ProductType type = types[rand.Next(0, 3)];


            if (index % 10 == 0 || index == 0)
            {
                batch = new()
                {
                    Value = $"Batch_{index}",
                    CreateAt = DateTime.Now.AddDays(-index)
                };
            }

            Product product = new()
            {
                Brand = brand,
                Type = type,
                Batch = batch!,
                Name = ProductName.Create($"name_{index}")
            };
            products.Add(product);
        }
        database.Products.AddRange(products);
        database.SaveChanges();
    }
}