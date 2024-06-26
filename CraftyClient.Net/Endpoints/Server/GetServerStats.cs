using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class GetServerStats
{
    public record Request(string ServerId);

    public class Handler(Request server) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}/stats")
                            .AddUrlSegment("id", server.ServerId);
    }

    public record Response(
    int StatsId,
    DateTime Created,
    Server ServerId,
    string Started,
    bool Running,
    double Cpu,
    [property: JsonConverter(typeof(AutoNumberToStringConverter))] string Mem,
    double MemPercent,
    string WorldName,
    string WorldSize,
    int ServerPort,
    string IntPingResults,
    int Online,
    int Max,
    string Players,
    string Desc,
    string? Icon,
    string Version,
    bool Updating,
    bool WaitingStart,
    bool FirstRun,
    bool Crashed,
    bool Importing);
}

public record Server(
    string ServerId,
    string Created,
    string ServerName,
    string Path,
    string BackupPath,
    string Executable,
    string LogPath,
    string ExecutionCommand,
    bool AutoStart,
    int AutoStartDelay,
    bool CrashDetection,
    string StopCommand,
    string ExecutableUpdateUrl,
    string ServerIp,
    int ServerPort,
    int LogsDeleteAfter,
    string Type,
    bool ShowStatus,
    int CreatedBy,
    int ShutdownTimeout,
    string IgnoredExits,
    bool CountPlayers
);