internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;

        var applicationAssembly = typeof(WaterShop.Goods.Application.ApplicationAssemblyReference).Assembly;

        services.AddMediatR(mediatorConf =>
        {
            mediatorConf.RegisterServicesFromAssembly(applicationAssembly);
        });
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}