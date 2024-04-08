using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class InvalidJsonException(RestResponse<StatusResponse> response) : CraftyException(response);
public class InvalidJsonException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
