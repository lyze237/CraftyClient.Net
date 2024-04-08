namespace CraftyClientNet.Models.Responses;

public record ExtendedUserResponse(
    int UserId,
    string Created,
    DateTime LastLogin,
    string LastUpdate,
    string LastIp,
    string Username,
    string Email,
    bool Enabled,
    bool Superuser,
    string Lang,
    string SupportLogs,
    string ServerOrder,
    bool Preparing,
    bool Hints,
    Role[] Roles
);

