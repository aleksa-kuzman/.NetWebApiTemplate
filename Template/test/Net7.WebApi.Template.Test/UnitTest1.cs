using Microsoft.AspNetCore.Mvc.Testing;

namespace Net7.WebApi.Template.Test;

public class Tests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        var application = new WebApplicationFactory<Program>();
        _client = application.CreateClient();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}