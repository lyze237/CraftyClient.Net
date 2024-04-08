namespace CraftyClientNet.Models.Requests;

public abstract record StartServerModel(
    string Name,
    ServerCreateType CreateType = ServerCreateType.MinecraftJava,
    MonitoringType MonitoringType = MonitoringType.MinecraftJava)
{
    public int[]? Roles { get; set; }
    public string? StopCommand { get; set; }
    public string? LogLocation { get; set; }
    public bool Crashdetection { get; set; } = false;
    public bool Autostart { get; set; } = false;
    public int AutostartDelay { get; set; } = 10;
}

public enum MonitoringType
{
    None,
    MinecraftJava,
    MinecraftBedrock
}

public enum ServerCreateType
{
    MinecraftJava,
    MinecraftBedrock,
    Custom
}