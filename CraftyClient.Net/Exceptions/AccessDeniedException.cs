using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class AccessDeniedException(RestResponse<StatusResponse> response) : CraftyException(response);
public class AccessDeniedException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);