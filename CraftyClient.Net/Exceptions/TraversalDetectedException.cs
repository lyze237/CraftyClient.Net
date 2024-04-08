using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Exceptions;

public class TraversalDetectedException(RestResponse<StatusResponse> response) : CraftyException(response);
public class TraversalDetectedException<TData>(RestResponse<StatusResponse<TData>> response) : CraftyException<TData>(response);
