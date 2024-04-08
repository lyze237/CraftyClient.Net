namespace CraftyClientNet.Models.Responses;

public record StatusResponse(string Status, string? Error);
public record StatusResponse<TData>(string Status, string? Error, TData? Data);