using RestSharp;

namespace CraftyClientNet;

public interface ICraftyRequestHandler<TResponse>
{
    RestRequest GenerateRequest();
}