using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class ServerRunningException(RestResponse<StatusResponse> response) : CraftyException(response);
public class ServerRunningException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
