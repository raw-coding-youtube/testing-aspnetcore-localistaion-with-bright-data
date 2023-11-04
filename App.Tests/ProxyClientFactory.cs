using System.Net;

namespace App.Tests;

public class ProxyClientFactory
{
    public HttpClient Create(string country)
    {
        return new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy("https://brd.superproxy.io", 22225)
                {
                    Credentials = new NetworkCredential(
                        $"...-zone-residential-country-{country}",
                        "..."
                    )
                },
            }
        );
    }
}