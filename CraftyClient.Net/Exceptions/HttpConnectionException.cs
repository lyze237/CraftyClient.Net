using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class HttpConnectionException(RestResponse<StatusResponse> response) : CraftyException(response);
public class HttpConnectionException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);