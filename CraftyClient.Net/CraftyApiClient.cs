using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using CraftyClientNet.auth;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models.Responses;
using RestSharp;
using RestSharp.Serializers.Json;

namespace CraftyClientNet;

public partial class CraftyApiClient : ICraftyApiClient
{
    private RestClient client;
    public bool LogRequests { get; set; }

    public CraftyApiClient(string baseUrl, string token, bool ignoreSslCerts = false, ConfigureHeaders? defaultHeaders = null) : this(new CraftyAuthenticator(baseUrl, token, ignoreSslCerts), ignoreSslCerts, defaultHeaders)
    {
    }


    public CraftyApiClient(string baseUrl, string username, string password, bool ignoreSslCerts = false, ConfigureHeaders? defaultHeaders = null) :
        this(new CraftyAuthenticator(baseUrl, username, password, ignoreSslCerts), ignoreSslCerts, defaultHeaders)
    {
    }

    private CraftyApiClient(CraftyAuthenticator authenticator, bool ignoreSslCerts = false, ConfigureHeaders? defaultHeaders = null)
    {
        var options = ignoreSslCerts
            ? new RestClientOptions(authenticator.BaseUrl) { Authenticator = authenticator, RemoteCertificateValidationCallback = (_, _, _, _) => true }
            : new RestClientOptions(authenticator.BaseUrl) { Authenticator = authenticator };

        client = new RestClient(options, defaultHeaders,
            config => config.UseSystemTextJson(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    Converters =
                    {
                        new FlagsToBitStringConverter(),
                        new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower)
                    },
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }));
    }

    private async Task<TData> ExecuteAsync<TData>(RestRequest request)
    {
        if (LogRequests)
            request.OnAfterRequest += async response =>
            {
                var message = response.RequestMessage!;
                Console.WriteLine($"[{message.Method} {response.StatusCode}] {message.RequestUri} {(message.Content != null ? $" => {await message.Content!.ReadAsStringAsync()}" : "")}\n <= {await response.Content.ReadAsStringAsync()}");
            };
        
        var response = await client.ExecuteAsync<StatusResponse<TData>>(request);
        return response.CheckSendyError().Data!;
    }
    
    private async Task<RestResponse<StatusResponse>> ExecuteAsync(RestRequest request)
    {
        if (LogRequests)
            request.OnAfterRequest += async response =>
            {
                var message = response.RequestMessage!;
                Console.WriteLine($"[{message.Method} {response.StatusCode}] {message.RequestUri} {(message.Content != null ? $" => {await message.Content!.ReadAsStringAsync()}" : "")}\n <= {await response.Content.ReadAsStringAsync()}");
            };
        
        var response = await client.ExecuteAsync<StatusResponse>(request);
        response.CheckSendyError();

        return response;
    }

    public void Dispose()
    {
        client.Dispose();
        GC.SuppressFinalize(this);
    }
}