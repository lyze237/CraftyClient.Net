using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class MissingParametersException(RestResponse<StatusResponse> response) : CraftyException(response);
public class MissingParametersException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
