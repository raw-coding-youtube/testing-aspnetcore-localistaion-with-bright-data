using System.Net.Http.Json;

namespace App.Tests;

public class ProxyTest(ProxyClientFactory clientFactory) : IClassFixture<ProxyClientFactory>
{
    [Theory]
    [InlineData("gb", "Hello World")]
    [InlineData("es", "Hola Mundo")]
    public async Task TestContent(string country, string expected)
    {
        var client = clientFactory.Create(country);
        var result = await client.GetStringAsync("https://brightdata-231028155802.azurewebsites.net?check_ip");

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "gb", "£20.00")]
    [InlineData(2, "gb", "£30.00")]
    [InlineData(1, "es", "€25.00")]
    [InlineData(2, "es", "€33.00")]
    public async Task TestProducts(int productId, string country, string expected)
    {
        var client = clientFactory.Create(country);
        
        var result = await client.GetFromJsonAsync<ProductProjection>(
            $"https://brightdata-231028155802.azurewebsites.net/products/{productId}?check_ip"
        );

        Assert.NotNull(result);
        Assert.Equal(expected, result.ValueDescription);
    }
}