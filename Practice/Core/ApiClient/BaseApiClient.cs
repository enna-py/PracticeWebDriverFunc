using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.ApiClient;
public class BaseApiClient
{
    private readonly RestClient _client;
    private readonly JsonSerializerOptions _jsonOptions;


    public BaseApiClient(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl)
        {
            ThrowOnAnyError = false
        };
        _client = new RestClient(options);


        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }


    public async Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        return await _client.ExecuteAsync(request).ConfigureAwait(false);
    }


    public T? Deserialize<T>(string? content)
    {
        if (string.IsNullOrWhiteSpace(content)) return default;
        return JsonSerializer.Deserialize<T>(content, _jsonOptions);
    }


    public static bool IsSuccess(HttpStatusCode code)
    => ((int)code >= 200 && (int)code < 300);
}
