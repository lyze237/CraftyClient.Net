using System.Text.RegularExpressions;
using System.Web;
using CraftyClientNet.Endpoints.Server;
using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<GetServers.Response[]> GetServers() =>
        await ExecuteAsync(new GetServers.Handler());

    public async Task<StartServerResponse> CreateServer(StartServerModel request) =>
        await ExecuteAsync<StartServerResponse>(new RestRequest("api/v2/servers", Method.Post)
            .AddJsonBody(request));

    public async Task<bool> WaitForServerImport(string serverId, CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = await GetServerStats(new GetServerStats.Request(serverId));
            if (!response.Importing)
            {
                // ReSharper disable once MethodSupportsCancellation
#pragma warning disable CA2016
                await Task.Delay(5000); // Apparently crafty does some stuff after import = false, so let's wait a couple seconds to make sure it's actually done importing.
#pragma warning restore CA2016
                return true;
            }

            await Task.Delay(1000, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return false;
        }
    }

    public async Task<bool> WaitForServerStart(string serverId, CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = await GetServerStats(new GetServerStats.Request(serverId));
            if (response.Running)
            {
                // ReSharper disable once MethodSupportsCancellation
#pragma warning disable CA2016
                await Task.Delay(5000); // Apparently crafty does some stuff after import = false, so let's wait a couple seconds to make sure it's actually done importing.
#pragma warning restore CA2016
                return true;
            }

            await Task.Delay(1000, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return false;
        }
    }
    
        public async Task<bool> WaitForServerDone(string serverId, CancellationToken cancellationToken, string regex = @"Done \(.+s\)! For help, type ""help""")
        {
            while (true)
            {
                var response = await GetServerLogs(new GetServerLogs.Request(serverId));
                if (response.Any(line => Regex.IsMatch(HttpUtility.HtmlDecode(line), regex)))
                    return true;
    
                await Task.Delay(1000, cancellationToken);
    
                if (cancellationToken.IsCancellationRequested)
                    return false;
            }
        }

    public async Task<GetServerStats.Response> GetServerStats(GetServerStats.Request request) =>
        await ExecuteAsync(new GetServerStats.Handler(request));

    public async Task<SendActionToServer.Response> SendActionToServer(SendActionToServer.Request request) =>
        await ExecuteAsync(new SendActionToServer.Handler(request));
    
    public async Task<DeleteServer.Response> DeleteServer(DeleteServer.Request request) =>
        await ExecuteAsync(new DeleteServer.Handler(request));

    public async Task<SendStdInToServer.Response> SendStdInToServer(SendStdInToServer.Request request) =>
        await ExecuteAsync(new SendStdInToServer.Handler(request));

    public async Task<string[]> GetServerLogs(GetServerLogs.Request request) =>
        await ExecuteAsync(new GetServerLogs.Handler(request));

    public async Task<GetServerPublicData.Response> GetServerPublicData(GetServerPublicData.Request request) =>
        await ExecuteAsync(new GetServerPublicData.Handler(request));
}