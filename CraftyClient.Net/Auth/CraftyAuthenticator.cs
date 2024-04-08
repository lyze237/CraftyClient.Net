using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace CraftyClientNet.auth;

public class CraftyAuthenticator : AuthenticatorBase
{
    public string BaseUrl { get; }
    private readonly string? username, password;
    private readonly bool ignoreSslCerts;

    public CraftyAuthenticator(string baseUrl, string token, bool ignoreSslCerts) : base(token)
    {
        BaseUrl = baseUrl;
        this.ignoreSslCerts = ignoreSslCerts;
    }

    public CraftyAuthenticator(string baseUrl, string username, string password, bool ignoreSslCerts) : base("")
    {
        BaseUrl = baseUrl;
        
        this.username = username;
        this.password = password;
        
        this.ignoreSslCerts = ignoreSslCerts;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrWhiteSpace(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }

    private async Task<string> GetToken()
    {
        var options = ignoreSslCerts
            ? new RestClientOptions(BaseUrl) { RemoteCertificateValidationCallback = (_, _, _, _) => true }
            : new RestClientOptions(BaseUrl);

        var client = new RestClient(options);
        
        var request = new RestRequest("api/v2/auth/login", Method.Post);
        request.AddJsonBody(new
        { 
            username, 
            password
        });
        
        var response = await client.ExecuteAsync<StatusResponse<LoginResponse>>(request);
        return response.CheckSendyError().Data!.Token;
    }
}