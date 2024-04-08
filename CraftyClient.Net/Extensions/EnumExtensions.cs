using System.Text;

namespace CraftyClientNet.Extensions;

public static class EnumExtensions
{
    public static string To_snake_case<T>(this T value) where T : Enum
    {
        var text = value.ToString();

        if (text.Length < 2)
            return text;

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (var i = 1; i < text.Length; ++i)
        {
            var c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}