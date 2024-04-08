using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace CraftyClientNet.Models.Requests;

public record CreateUserModel(
    string Username,
    string Password,
    string Email,
    int? Manager = null,
    int[]? Roles = null,
    [property: JsonIgnore] int? ServerCreationPermission = null,
    [property: JsonIgnore] int? UserConfigPermission = null,
    [property: JsonIgnore] int? RolesConfigPermission = null,
    bool Superuser = false,
    string Theme = "default",
    string Lang = "en",
    bool Enabled = true,
    bool Hints = true)
{
    public List<UserPermission> Permissions {
        get
        {
            var list = new List<UserPermission>();
            if (ServerCreationPermission.HasValue)
                list.Add(new UserPermission("SERVER_CREATION", ServerCreationPermission));
            if (UserConfigPermission.HasValue)
                list.Add(new UserPermission("USER_CONFIG", UserConfigPermission));
            if (RolesConfigPermission.HasValue)
                list.Add(new UserPermission("ROLES_CONFIG", RolesConfigPermission));
            return list;
        }
    }
}

public record UserPermission(string Name, int? Quantity)
{
    public bool Enabled => Quantity != null;
}