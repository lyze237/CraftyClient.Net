using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class ServerNotFoundException(RestResponse<StatusResponse> response) : CraftyException(response);
public class ServerNotFoundException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
