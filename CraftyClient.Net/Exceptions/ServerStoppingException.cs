using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class ServerStoppingException(RestResponse<StatusResponse> response) : CraftyException(response);
public class ServerStoppingException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
