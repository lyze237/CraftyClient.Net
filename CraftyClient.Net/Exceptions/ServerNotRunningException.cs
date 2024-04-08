using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class ServerNotRunningException(RestResponse<StatusResponse> response) : CraftyException(response);
public class ServerNotRunningException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);