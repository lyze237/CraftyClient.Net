using System.Text.Json;
using System.Text.Json.Serialization;

namespace CraftyClientNet.Converters;

public class AutoNumberToStringConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert) =>
        typeof(string) == typeToConvert;

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.TryGetInt64(out var l) ? l.ToString() : reader.GetDouble().ToString();
            case JsonTokenType.String:
                return reader.GetString();
        }

        using var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone().ToString();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options) => 
        writer.WriteStringValue(value.ToString());
}