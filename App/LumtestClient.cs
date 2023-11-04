namespace App;

public class LumtestClient
{
    private readonly HttpClient _client;

    public LumtestClient(HttpClient client)
    {
        _client = client;
    }

    public Task<IpInfo?> GetIpInfoAsync(string ip)
    {
        var query = string.IsNullOrEmpty(ip) ? "" : $"?ip={ip}";
        return _client.GetFromJsonAsync<IpInfo>($"/myip.json{query}");
    }
}

public class IpInfo
{
    public string Ip { get; set; } = "";
    public string Country { get; set; } = "";
}