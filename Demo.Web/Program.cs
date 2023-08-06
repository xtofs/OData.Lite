using OData.Lite;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddEndpointsApiExplorer();

        // builder.Services.AddSwaggerGen();

        if (!Model.TryLoad("example89.csdl.xml", out var model))
        {
            return;
        };
        var schema = model.Schemas.First();

        var app = ODataApplication.Build(builder, model, schema);
       
        app.MapGet("/products", () => Results.Ok());
        app.MapGet("/products/{id}", () => Results.Ok());
        app.MapGet("/products/{id}/category/products/{id1}/category", (string id, string id1) => Results.Json(new { id, id1}));


        app.Run();
    }
}
