using Microsoft.Extensions.Localization;

namespace App;

public class ContentEndpoint
{
    public static string Handle(
        IStringLocalizer<ContentEndpoint> localizer
    )
    {
        return localizer["HelloWorld"];
    }
}