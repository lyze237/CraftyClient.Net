using CraftyClientNet.Endpoints.Roles;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<GetRoles.Response> GetRoles() =>
        await ExecuteAsync(new GetRoles.Handler());

    public async Task<GetRole.Response> GetRole(GetRole.Request role) => 
        await ExecuteAsync(new GetRole.Handler(role));

    public async Task<CreateRole.Response> CreateRole(CreateRole.Request role) =>
        await ExecuteAsync(new CreateRole.Handler(role));
}