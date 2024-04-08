namespace CraftyClientNet.Models.Responses;

public record BasicUserResponse(
    int UserId,
    string Created,
    string Username,
    bool Enabled,
    bool Superuser,
    string Lang
);