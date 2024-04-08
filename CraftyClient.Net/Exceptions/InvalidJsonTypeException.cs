using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class InvalidJsonTypeException(RestResponse<StatusResponse> restResponse) : CraftyException(restResponse);
public class InvalidJsonTypeException<TData>(RestResponse<StatusResponse<TData>> restResponse) : CraftyException<TData>(restResponse);