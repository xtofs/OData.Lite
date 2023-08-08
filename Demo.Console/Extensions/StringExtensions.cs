namespace System.Collections.Generic;

using System.Text.RegularExpressions;


public static partial class StringExtensions
{
    public static bool TryGetKeyName(this string segment, [MaybeNullWhen(false)] out string keyName)
    {
        var match = KeyNameRegex().Match(segment);
        if (match.Success)
        {
            keyName = match.Groups[1].Value;
            return true;
        }
        keyName = default;
        return false;
    }


    // [GeneratedRegex(@"{\w+\.(\w+): [^}]+}")]
    [GeneratedRegex(@"{(\w+)(: [^}]+})?")]
    private static partial Regex KeyNameRegex();
}