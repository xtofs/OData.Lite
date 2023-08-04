public static class AppExtensions
{
    public static void MapService(this WebApplication app)
    {
        app.MapGet("products", 
			(IODataService service) => 
				service.Get("products", new Dictionary<string, string> {  }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}", 
			(IODataService service, string id) => 
				service.Get("products/{id}", new Dictionary<string, string> { ["id"]=id }, "Product"));
        app.MapGet("products/{id}/category", 
			(IODataService service, string id) => 
				service.Get("products/{id}/category", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Category"));
        app.MapGet("products/{id}/category/products", 
			(IODataService service, string id) => 
				service.Get("products/{id}/category/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/category/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("products/{id}/category/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/category/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/category/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("products/{id}/category/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/category/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/category/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/category/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("products/{id}/category/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/category/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("products/{id}/supplier", 
			(IODataService service, string id) => 
				service.Get("products/{id}/supplier", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/supplier/products", 
			(IODataService service, string id) => 
				service.Get("products/{id}/supplier/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/supplier/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("products/{id}/supplier/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/supplier/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/supplier/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("products/{id}/supplier/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/supplier/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("products/{id}/supplier/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("products/{id}/supplier/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("products/{id}/supplier/address", 
			(IODataService service, string id) => 
				service.Get("products/{id}/supplier/address", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Address"));
        app.MapGet("products/{id}/supplier/address/country", 
			(IODataService service, string id) => 
				service.Get("products/{id}/supplier/address/country", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Country"));
        app.MapGet("categories", 
			(IODataService service) => 
				service.Get("categories", new Dictionary<string, string> {  }, "[ODataDemo.Category]"));
        app.MapGet("categories/{id}", 
			(IODataService service, string id) => 
				service.Get("categories/{id}", new Dictionary<string, string> { ["id"]=id }, "Category"));
        app.MapGet("categories/{id}/products", 
			(IODataService service, string id) => 
				service.Get("categories/{id}/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("categories/{id}/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("categories/{id}/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("categories/{id}/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("categories/{id}/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("categories/{id}/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("categories/{id}/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("categories/{id}/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("categories/{id}/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("categories/{id}/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("categories/{id}/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("categories/{id}/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("categories/{id}/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("categories/{id}/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("categories/{id}/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("categories/{id}/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("suppliers", 
			(IODataService service) => 
				service.Get("suppliers", new Dictionary<string, string> {  }, "[ODataDemo.Supplier]"));
        app.MapGet("suppliers/{id}", 
			(IODataService service, string id) => 
				service.Get("suppliers/{id}", new Dictionary<string, string> { ["id"]=id }, "Supplier"));
        app.MapGet("suppliers/{id}/products", 
			(IODataService service, string id) => 
				service.Get("suppliers/{id}/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("suppliers/{id}/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("suppliers/{id}/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("suppliers/{id}/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("suppliers/{id}/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("suppliers/{id}/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("suppliers/{id}/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("suppliers/{id}/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("suppliers/{id}/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("mainSupplier", 
			(IODataService service) => 
				service.Get("mainSupplier", new Dictionary<string, string> {  }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products", 
			(IODataService service) => 
				service.Get("mainSupplier/products", new Dictionary<string, string> {  }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}", new Dictionary<string, string> { ["id"]=id }, "Product"));
        app.MapGet("mainSupplier/products/{id}/category", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/category", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/category/products", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/category/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("mainSupplier/products/{id}/category/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/category/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("mainSupplier/products/{id}/supplier", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/supplier", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/supplier/products", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/supplier/products", new Dictionary<string, string> { ["id"]=id }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/category", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/category/products", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/category/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/category/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "[ODataDemo.Product]"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "Product"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}/category", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}/category", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Category"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}/supplier", 
			(IODataService service, string id, string id1, string id2) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/products/{id2}/supplier", new Dictionary<string, string> { ["id"]=id,["id1"]=id1,["id2"]=id2 }, "ODataDemo.Supplier"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/address", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/address", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Address"));
        app.MapGet("mainSupplier/products/{id}/supplier/products/{id1}/supplier/address/country", 
			(IODataService service, string id, string id1) => 
				service.Get("mainSupplier/products/{id}/supplier/products/{id1}/supplier/address/country", new Dictionary<string, string> { ["id"]=id,["id1"]=id1 }, "ODataDemo.Country"));
        app.MapGet("mainSupplier/products/{id}/supplier/address", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/supplier/address", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Address"));
        app.MapGet("mainSupplier/products/{id}/supplier/address/country", 
			(IODataService service, string id) => 
				service.Get("mainSupplier/products/{id}/supplier/address/country", new Dictionary<string, string> { ["id"]=id }, "ODataDemo.Country"));
        app.MapGet("mainSupplier/address", 
			(IODataService service) => 
				service.Get("mainSupplier/address", new Dictionary<string, string> {  }, "ODataDemo.Address"));
        app.MapGet("mainSupplier/address/country", 
			(IODataService service) => 
				service.Get("mainSupplier/address/country", new Dictionary<string, string> {  }, "ODataDemo.Country"));
        app.MapGet("countries", 
			(IODataService service) => 
				service.Get("countries", new Dictionary<string, string> {  }, "[ODataDemo.Country]"));
        app.MapGet("countries/{code}", 
			(IODataService service, string code) => 
				service.Get("countries/{code}", new Dictionary<string, string> { ["code"]=code }, "Country"));
    }
}
