using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class ApiHandlerNotFoundException(RestResponse<StatusResponse> response) : CraftyException(response);
public class ApiHandlerNotFoundException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);