using Microsoft.AspNetCore.Localization;

namespace App;

public static class HttpContextExtensions
{
    public static string GetCulture(this HttpContext @this)
    {
        return @this.Features.Get<IRequestCultureFeature>()?.RequestCulture.UICulture.Name ?? "en";
    }
}