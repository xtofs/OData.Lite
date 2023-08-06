using Microsoft.AspNetCore.OpenApi;
using OData.Lite;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddEndpointsApiExplorer();

        // builder.Services.AddSwaggerGen();
        // builder.Services.AddSingleton<IODataService, DemoService>();

        if (!Model.TryLoad("example89.csdl.xml", out var model))
        {
            return;
        };
        var schema = model.Schemas.First();

        var app = ODataApplication.Build(builder, model, schema);
       
        app.MapGet("/products", () => Results.Ok());
        app.MapGet("/products/{id}", () => Results.Ok());

        app.Run();
    }
}
