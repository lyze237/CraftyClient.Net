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

    public async Task<ServerStatsResponse> GetServerStats(string id) =>
        await ExecuteAsync<ServerStatsResponse>(
            new RestRequest("api/v2/servers/{id}/stats")
                .AddUrlSegment("id", id));

    public async Task<RestResponse> SendActionToServer(string serverId, ServerAction action) =>
        await ExecuteAsync(new RestRequest("api/v2/servers/{id}/action/{action}", Method.Post)
            .AddUrlSegment("id", serverId)
            .AddUrlSegment("action", action.To_snake_case()));
}