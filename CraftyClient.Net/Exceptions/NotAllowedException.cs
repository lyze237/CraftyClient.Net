using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class NotAllowedException(RestResponse<StatusResponse> response) : CraftyException(response);
public class NotAllowedException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
