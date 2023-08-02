public static class DebugExtensions
{
    public static void Debug(this TextWriter writer, object obj)
    {
        var type = obj.GetType();
        writer.WriteLine("{0}", type.Name);
        foreach (var prop in type.GetProperties())
        {
            writer.Debug("    ", prop.Name, prop.GetValue(obj));
        }
    }

    public static void Debug(this TextWriter writer, string indent, string name, object value)
    {
        var type = value.GetType();
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Object when type.IsAssignableTo(typeof(IEnumerable<object>)):
                foreach (var item in (value as IEnumerable<object>)!)
                {
                    writer.Debug(indent, "", item);
                }
                break;
            case TypeCode.Object:
                writer.WriteLine("{0}{1}: {2}", indent, name, type.Name);
                indent += "    ";
                foreach (var prop in type.GetProperties())
                {
                    writer.Debug(indent, prop.Name, prop.GetValue(value));
                }
                break;

            case TypeCode.Boolean:
            case TypeCode.Int32:
            case TypeCode.String:
            case TypeCode.DateTime:
                writer.WriteLine("{0}{1}: {2}", indent, name, value);
                break;
        }

    }
}