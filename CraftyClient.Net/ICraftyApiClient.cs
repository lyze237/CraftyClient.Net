using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public interface ICraftyApiClient : IDisposable
{
    Task<Role[]> GetRoles();
    Task<Role> GetRole(int id);
    Task<CreateRoleResponse> CreateRole(string name, (string serverId, ServerPermissions permissions)[]? servers = null);
    
    Task<StartServerResponse> CreateServer(StartServerModel server);
    Task<ServerStatsResponse> GetServerStats(string serverId);
    Task<RestResponse> SendActionToServer(string serverId, ServerAction action);

    Task<BasicUserResponse[]> GetUsers();
    Task<CreateUserResponse> CreateUser(CreateUserModel user);
    Task<ExtendedUserResponse> GetUser(int userId);
    Task DeleteUser(int userId);
    // Task ModifyUser();
    Task<GetUserPermissionsResponse> GetUserPermissions(int userId);
}