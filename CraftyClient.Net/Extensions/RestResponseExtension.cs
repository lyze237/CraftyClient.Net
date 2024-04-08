using System.Net;
using CraftyClientNet.Exceptions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Extensions;

public static class RestResponseExtension
{
    public static StatusResponse CheckSendyError(this RestResponse<StatusResponse> response)
    {
        if (response.Data == null)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                throw new InvalidJsonTypeException(response);
            
            throw new HttpConnectionException(response);
        }

        if (response.Data.Status == "ok")
            return response.Data;

        switch (response.Data.Error)
        {
            case "ACCESS_DENIED":
            case "NOT_AUTHORIZED":
            case "INCORRECT_CREDENTIALS":
                throw new AccessDeniedException(response);
            case "SER_NOT_RUNNING":
                throw new ServerNotRunningException(response);
            case "SER_STOP_CALLED":
            case "SER_RESTART_CALLED":
                throw new ServerStoppingException(response);
            case "NO_COMMAND":
            case "MISSING_PARAMS":
                throw new MissingParametersException(response);
            case "INVALID_JSON":
            case "INVALID_JSON_SCHEMA":
                throw new InvalidJsonException(response);
            case "TRAVERSAL DETECTED":
                throw new TraversalDetectedException(response);
            case "SER_RUNNING":
                throw new ServerRunningException(response);
            case "NOT_ALLOWED":
                throw new NotAllowedException(response);
            case "NOT_FOUND":
                throw new ServerNotFoundException(response);
            case "API_HANDLER_NOT_FOUND":
                throw new ApiHandlerNotFoundException(response);
            default:
                throw new ArgumentException($"Unknown error {response.Data.Error}", nameof(response.Data.Error));
        }
    }
    
    public static StatusResponse<TData> CheckSendyError<TData>(this RestResponse<StatusResponse<TData>> response)
    {
        if (response.Data == null)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                throw new InvalidJsonTypeException<TData>(response);
            
            throw new HttpConnectionException<TData>(response);
        }

        if (response.Data.Status == "ok")
            return response.Data;

        switch (response.Data.Error)
        {
            case "ACCESS_DENIED":
            case "NOT_AUTHORIZED":
            case "INCORRECT_CREDENTIALS":
                throw new AccessDeniedException<TData>(response);
            case "SER_NOT_RUNNING":
                throw new ServerNotRunningException<TData>(response);
            case "SER_STOP_CALLED":
            case "SER_RESTART_CALLED":
                throw new ServerStoppingException<TData>(response);
            case "NO_COMMAND":
            case "MISSING_PARAMS":
                throw new MissingParametersException<TData>(response);
            case "INVALID_JSON":
            case "INVALID_JSON_SCHEMA":
                throw new InvalidJsonException<TData>(response);
            case "TRAVERSAL DETECTED":
                throw new TraversalDetectedException<TData>(response);
            case "SER_RUNNING":
                throw new ServerRunningException<TData>(response);
            case "NOT_ALLOWED":
                throw new NotAllowedException<TData>(response);
            case "NOT_FOUND":
                throw new ServerNotFoundException<TData>(response);
            case "API_HANDLER_NOT_FOUND":
                throw new ApiHandlerNotFoundException<TData>(response);
            default:
                throw new ArgumentException($"Unknown error {response.Data.Error}", nameof(response.Data.Error));
        }
    }
}