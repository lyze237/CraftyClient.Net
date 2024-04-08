using CraftyClientNet.Models.Permissions;

namespace CraftyClientNet.Models.Responses;

public record GetUserPermissionsResponse(ServerPermissions Permissions, Dictionary<string, int> Counters, Dictionary<string, int> Limits);