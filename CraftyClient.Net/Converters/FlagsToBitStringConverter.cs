using System.Text.Json;
using System.Text.Json.Serialization;
using CraftyClientNet.Models.Permissions;
using RestSharp.Extensions;

namespace CraftyClientNet.Converters;

public class FlagsToBitStringConverter : JsonConverter<ServerPermissions>
{
    public override bool CanConvert(Type typeToConvert) => 
        typeToConvert.IsEnum && typeToConvert.GetAttribute<FlagsAttribute>() != null;

    public override ServerPermissions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        (ServerPermissions) Convert.ToInt32(reader.GetString(), 2);

    public override void Write(Utf8JsonWriter writer, ServerPermissions value, JsonSerializerOptions options) =>
        writer.WriteStringValue(Convert.ToString((int) value, 2));
}