using Microsoft.AspNetCore.Localization;

namespace App;

public class LumtestLocationProvider : RequestCultureProvider
{
    
    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (!httpContext.Request.Query.ContainsKey("check_ip"))
        {
            return null;
        }

        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        if (string.IsNullOrEmpty(ipAddress))
        {
            return null;
        }
        
        var lumtest = httpContext.RequestServices.GetRequiredService<LumtestClient>();

        var ipInfo = await lumtest.GetIpInfoAsync(ipAddress);
        
        return ipInfo == null ? null: new ProviderCultureResult(ipInfo.Country.ToLower());
    }
}