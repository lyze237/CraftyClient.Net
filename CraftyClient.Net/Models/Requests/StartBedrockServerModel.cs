namespace CraftyClientNet.Models.Requests;

public record StartBedrockServerModel(
    string Name) : StartServerModel(Name, ServerCreateType.MinecraftBedrock, MonitoringType.MinecraftBedrock)
{
    public BedrockMonitoringData MinecraftBedrockMonitoringData { get; set; }
}

public record BedrockMonitoringData(string Host = "127.0.0.1", int Port = 19132);
