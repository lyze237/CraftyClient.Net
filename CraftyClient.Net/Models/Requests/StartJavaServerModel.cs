using System.Text.Json.Serialization;

namespace CraftyClientNet.Models.Requests;

public record StartJavaServerModel(
    string Name,
    [property: JsonIgnore] JavaCreateData JavaCreateData
) : StartServerModel(Name, ServerCreateType.MinecraftJava, MonitoringType.MinecraftJava)
{
    public  MinecraftJavaCreateDataType MinecraftJavaCreateData => JavaCreateData switch
    {
        JavaDownloadJarData jar => new JavaDownloadJarWrapper(jar),
        JavaImportServerData server => new JavaImportServerWrapper(server),
        JavaImportZipData zip => new JavaImportZipWrapper(zip),
        _ => throw new ArgumentException("Undefined wrapper type", nameof(JavaCreateData))
    };
    public JavaMonitoringData MinecraftJavaMonitoringData { get; set; } = new();
}

public record JavaMonitoringData(string Host = "127.0.0.1", int Port = 25565);

[JsonDerivedType(typeof(JavaDownloadJarWrapper))]
[JsonDerivedType(typeof(JavaImportServerWrapper))]
[JsonDerivedType(typeof(JavaImportZipWrapper))]
public abstract record MinecraftJavaCreateDataType(JavaCreateType CreateType);

public record JavaDownloadJarWrapper(JavaDownloadJarData DownloadJarCreateData) : MinecraftJavaCreateDataType(JavaCreateType.DownloadJar);
public record JavaImportServerWrapper(JavaImportServerData ImportServerCreateData) : MinecraftJavaCreateDataType(JavaCreateType.ImportServer);
public record JavaImportZipWrapper(JavaImportZipData ImportZipCreateData) : MinecraftJavaCreateDataType(JavaCreateType.ImportZip);

public abstract record JavaCreateData(
    int MemMin,
    int MemMax,
    int ServerPropertiesPort,
    bool AgreeToEula,
    [property: JsonIgnore] JavaCreateType CreateType);

public record JavaDownloadJarData(
    string Category,
    string Type,
    string Version,
    int MemMin,
    int MemMax,
    int ServerPropertiesPort,
    bool AgreeToEula)
    : JavaCreateData(MemMin, MemMax, ServerPropertiesPort, AgreeToEula, JavaCreateType.DownloadJar);

public record JavaImportServerData(
    string ExistingServerPath,
    string Jarfile,
    int MemMin,
    int MemMax,
    int ServerPropertiesPort,
    bool AgreeToEula)
    : JavaCreateData(MemMin, MemMax, ServerPropertiesPort, AgreeToEula, JavaCreateType.ImportServer);

public record JavaImportZipData(
    string ZipPath,
    string ZipRoot,
    string Jarfile,
    int MemMin,
    int MemMax,
    int ServerPropertiesPort,
    bool AgreeToEula)
    : JavaCreateData(MemMin, MemMax, ServerPropertiesPort, AgreeToEula, JavaCreateType.ImportZip);

public enum JavaCreateType
{
    DownloadJar,
    ImportServer,
    ImportZip
}