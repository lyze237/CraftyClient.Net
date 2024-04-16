using CraftyClientNet.Endpoints.Roles;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<GetRoles.Response[]> GetRoles() =>
        await ExecuteAsync(new GetRoles.Handler());

    public async Task<GetRole.Response> GetRole(GetRole.Request request) => 
        await ExecuteAsync(new GetRole.Handler(request));

    public async Task<CreateRole.Response> CreateRole(CreateRole.Request request) =>
        await ExecuteAsync(new CreateRole.Handler(request));

    public async Task<int[]> GetARolesUsers(GetARolesUsers.Request request) =>
        await ExecuteAsync(new GetARolesUsers.Handler(request));

    public async Task<int> DeleteRole(DeleteRole.Request request) =>
        await ExecuteAsync(new DeleteRole.Handler(request));
}