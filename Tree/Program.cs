using OData.Lite;

var filename = Debugger.IsAttached ? "../../../example89.csdl.xml" : "example89.csdl.xml";

if (Model.Load(filename, out var model))
{
    Console.WriteLine(model); ;
}

