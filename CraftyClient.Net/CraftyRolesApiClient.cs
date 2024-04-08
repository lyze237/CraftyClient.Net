using CraftyClientNet.Models.Permissions;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<Role[]> GetRoles() =>
        await ExecuteAsync<Role[]>(new RestRequest("api/v2/roles"));

    public async Task<Role> GetRole(int id) =>
        await ExecuteAsync<Role>(new RestRequest("api/v2/roles/{id}")
            .AddUrlSegment("id", id));

    public async Task<CreateRoleResponse> CreateRole(string name,
        (string serverId, ServerPermissions permissions)[]? servers = null)
    {
        var adjustedServers =
            (servers ?? Array.Empty<(string serverId, ServerPermissions permissions)>());
            /*
            .Select(s =>
                (s.serverId, Convert.ToString((int)s.permissions, 2)));*/

        return await ExecuteAsync<CreateRoleResponse>(new RestRequest("api/v2/roles", Method.Post)
            .AddJsonBody(new
            {
                name,
                servers = adjustedServers
            }));
    }
}