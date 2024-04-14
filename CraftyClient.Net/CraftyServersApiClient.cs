using CraftyClientNet.Endpoints.Server;
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
            var response = await GetServerStats(new GetServerStats.Request(serverId));
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
            var response = await GetServerStats(new GetServerStats.Request(serverId));
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

    public async Task<GetServerStats.Response> GetServerStats(GetServerStats.Request server) => 
        await ExecuteAsync(new GetServerStats.Handler(server));

    public async Task<SendActionToServer.Response> SendActionToServer(SendActionToServer.Request action) => 
        await ExecuteAsync(new SendActionToServer.Handler(action));
}