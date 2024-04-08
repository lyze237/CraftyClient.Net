using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp.Extensions;

namespace CraftyClientNet.Converters;

public class FlagsToBitStringConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum && typeToConvert.GetAttribute<FlagsAttribute>() != null;
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return Convert.ToInt32(reader.GetString(), 2);
        }

        using var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone().ToString();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options) => 
        writer.WriteStringValue(Convert.ToString((int) value, 2));
}