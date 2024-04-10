using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<StartServerResponse> CreateServer(StartServerModel server) =>
        await ExecuteAsync<StartServerResponse>(new RestRequest("api/v2/servers", Method.Post)
            .AddJsonBody(server));

    public async Task<bool> WaitForServerImport(string serverId, CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = await GetServerStats(serverId);
            if (!response.Importing)
            {
                 await Task.Delay(5000); // Apparently crafty does some stuff after import = false, so let's wait a couple seconds to make sure it's actually done importing.
                 return true;
            }

            await Task.Delay(500, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested)
                return false;
        }
    }

    public async Task<bool> WaitForServerStart(string serverId, CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = await GetServerStats(serverId);
            if (response.Running)
            {
                 await Task.Delay(5000); // Apparently crafty does some stuff after import = false, so let's wait a couple seconds to make sure it's actually done importing.
                 return true;
            }

            await Task.Delay(500, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested)
                return false;
        }
    }

    public async Task<ServerStatsResponse> GetServerStats(string id) =>
        await ExecuteAsync<ServerStatsResponse>(
            new RestRequest("api/v2/servers/{id}/stats")
                .AddUrlSegment("id", id));

    public async Task<RestResponse> SendActionToServer(string serverId, ServerAction action) =>
        await ExecuteAsync(new RestRequest("api/v2/servers/{id}/action/{action}", Method.Post)
            .AddUrlSegment("id", serverId)
            .AddUrlSegment("action", action.To_snake_case()));
}