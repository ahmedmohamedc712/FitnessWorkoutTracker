using System.Net.Http.Headers;
using Domain.Entities;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.Extensions;

public static class HttpClientExtensions
{
    public static HttpClient CreateAuthenticatedClient(this CustomWebApplicationFactory factory, User user)
    {
        var client = factory.CreateClient();

        var token = ApiTokenHelper.GetToken(factory, user);

        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}
