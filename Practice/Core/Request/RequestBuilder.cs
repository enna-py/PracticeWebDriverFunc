using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request;
public class RequestBuilder
{
    private Method _method = Method.Get;
    private string _resource = string.Empty;
    private readonly Dictionary<string, string> _headers = new();
    private readonly Dictionary<string, string> _query = new();
    private object? _body;
    private TimeSpan? _timeoutMs;


    public RequestBuilder WithMethod(Method method)
    { _method = method; return this; }


    public RequestBuilder WithResource(string resource)
    { _resource = resource; return this; }


    public RequestBuilder AddHeader(string key, string value)
    { _headers[key] = value; return this; }


    public RequestBuilder AddQuery(string key, string value)
    { _query[key] = value; return this; }


    public RequestBuilder WithJsonBody(object body)
    { _body = body; return this; }


    public RequestBuilder WithTimeout(TimeSpan timeoutMs)
    { _timeoutMs = timeoutMs; return this; }


    public RestRequest Build()
    {
        if (string.IsNullOrWhiteSpace(_resource))
            throw new InvalidOperationException("Resource must be set.");


        var req = new RestRequest(_resource, _method);


        foreach (var h in _headers)
            req.AddOrUpdateHeader(h.Key, h.Value);


        foreach (var q in _query)
            req.AddQueryParameter(q.Key, q.Value);


        if (_body is not null)
            req.AddJsonBody(_body);


        if (_timeoutMs.HasValue)
            req.Timeout = _timeoutMs.Value;


        return req;
    }
}
