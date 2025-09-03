using BL.Models;
using Core.ApiClient;
using Core.Config;
using Core.Request;
using log4net;
using RestSharp;
using System.Net;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]

namespace MainPageTests.APITests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[Category("API")]
public class ApiTests
{
    private BaseApiClient _api = null!;
    private static readonly ILog Log = LogManagerHelper.ConfigureLogger();

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _api = new BaseApiClient("https://jsonplaceholder.typicode.com");
        Log.Info("[SETUP] Initialized API client with BaseUrl=https://jsonplaceholder.typicode.com");
    }

    private RestRequest BuildGetUsersRequest()
    {
        return new RequestBuilder()
        .WithMethod(Method.Get)
        .WithResource("/users")
        .AddHeader("Accept", "application/json")
        .Build();
    }

    [Test]
    public async Task GetUsers_ShouldReturn_200_OK()
    {
        Log.Info($"[TEST] Starting {TestContext.CurrentContext.Test.Name}");

        var request = BuildGetUsersRequest();
        Log.Info("[ACTION] Sending GET /users");

        var response = await _api.ExecuteAsync(request);
        Log.Info("[ASSERT] Verifying HTTP status code");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
        $"Expected 200 OK but got {(int)response.StatusCode} {response.StatusDescription}.");

        Log.Info("[PASS] Received 200 OK");
    }

    [Test]
    public async Task GetUsers_ResponseHeader_ContentType_IsJson()
    {
        Log.Info($"[TEST] Starting {TestContext.CurrentContext.Test.Name}");

        var request = BuildGetUsersRequest();
        var response = await _api.ExecuteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK");

        var contentType = response.ContentType?.ToLowerInvariant();
        Assert.That(contentType, Does.Contain("application/json"),
            $"Expected Content-Type to contain 'application/json' but got '{contentType}'");
    }

    [Test]
    public async Task GetUsers_ShouldReturn_10UniqueUsersWithCompany()
    {
        Log.Info($"[TEST] Starting {TestContext.CurrentContext.Test.Name}");

        var response = await _api.ExecuteAsync(BuildGetUsersRequest());
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var users = _api.Deserialize<List<User>>(response.Content) ?? new();
        Assert.That(users.Count, Is.EqualTo(10), "Expected exactly 10 users");

        var distinctIds = users.Select(u => u.Id).Distinct().Count();
        Assert.That(distinctIds, Is.EqualTo(10), "IDs are not unique");

        foreach (var u in users)
        {
            Assert.That(u.Name, Is.Not.Null.And.Not.Empty, "Name is missing");
            Assert.That(u.Username, Is.Not.Null.And.Not.Empty, "Username is missing");
            Assert.That(u.Company?.Name, Is.Not.Null.And.Not.Empty, "Company.Name is missing");
        }
    }

    [Test]
    public async Task CreateUser_ShouldReturn_201Created_WithId()
    {
        Log.Info($"[TEST] Starting {TestContext.CurrentContext.Test.Name}");

        var newUser = new { Name = "Test User", Username = "testuser" };
        var request = new RequestBuilder()
            .WithMethod(Method.Post)
            .WithResource("/users")
            .AddHeader("Content-Type", "application/json")
            .WithJsonBody(newUser)
            .Build();

        var response = await _api.ExecuteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

        var created = _api.Deserialize<Dictionary<string, object>>(response.Content!);
        Assert.That(created, Does.ContainKey("id"), "Response does not contain ID");
    }

    [Test]
    public async Task InvalidEndpoint_ShouldReturn_404NotFound()
    {
        Log.Info($"[TEST] Starting {TestContext.CurrentContext.Test.Name}");

        var request = new RequestBuilder()
            .WithMethod(Method.Get)
            .WithResource("/invalidendpoint")
            .Build();

        var response = await _api.ExecuteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
