using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public abstract class CraftyException : Exception
{
    public RestResponse<StatusResponse> Response { get; }
    
    protected CraftyException(RestResponse<StatusResponse> response) : base($"{response.StatusCode} {(response.Data == null ? response.Content : $"{response.Data.Status}{(response.Data.Error == null ? "" : $" - {response.Data.Error}")}")}") => 
        Response = response;
}

public abstract class CraftyException<TData> : Exception
{
    public RestResponse<StatusResponse<TData>> Response { get; }
    
    protected CraftyException(RestResponse<StatusResponse<TData>> response) : base($"{response.StatusCode} {(response.Data == null ? response.Content : $"{response.Data.Status}{(response.Data.Error == null ? "" : $" - {response.Data.Error}")}")}") => 
        Response = response;
}