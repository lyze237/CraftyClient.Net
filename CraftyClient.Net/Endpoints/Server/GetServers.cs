using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class GetServers
{
    public class Handler : ICraftyRequestHandler<Response[]>
    {
        public RestRequest GenerateRequest() =>
            new("api/v2/servers");
    }

    public record Response(
        string ServerId,
        DateTime Created,
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
        bool CountPlayers);
}