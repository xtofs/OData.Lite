
using OData.Lite.EDM;

var m = new Model{
    new Schema("example.com", "self"),
    new Schema("vocab.example.com", null)
};

Console.WriteLine(m);
Console.WriteLine(m.GetSchema("self"));
Console.WriteLine(m.GetSchema("example.com"));

