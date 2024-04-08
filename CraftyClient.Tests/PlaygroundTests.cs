using System.Text.Json;
using System.Text.Json.Serialization;
using CraftyClientNet.Models.Requests;

namespace CraftyClientTests;

public class PlaygroundTests
{
    [Test]
    public async Task CreateServer()
    {
        var example = new StartJavaServerModel("Test",
            new JavaDownloadJarData("vanilla", "Paper", "1.20.4", 1024, 1024 * 4, 25565, true));

        var json = JsonSerializer.Serialize(example, new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower) }
        });
        Console.WriteLine(json);
    }
}