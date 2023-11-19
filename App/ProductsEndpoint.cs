using HyperTextExpression;
using HyperTextExpression.AspNetCore;
using static HyperTextExpression.HtmlExp;

namespace App;

public class ProductsEndpoint
{
    public static IResult Handle(
        HttpContext httpContext
    )
    {
        var culture = httpContext.GetCulture();

        var productsPrices = ProductEndpoint.ProductsDatabase
            .Select(product => (
                Product: product,
                Price: product.Prices.FirstOrDefault(x => x.Culture == culture)
                       ?? product.Prices.First()
            ))
            .ToList();

        return HtmlDoc(
                Head(
                    ("title", "products"),
                    Styles("https://cdn.jsdelivr.net/npm/bulma@0.9.4/css/bulma.min.css")
                ),
                Body(
                    Div(
                        Attrs("class", "container mt-5"),
                        Div(
                            Attrs("class", "columns"),
                            productsPrices.Select(x =>
                                Div(
                                    Attrs("class", "column is-3"),
                                    RenderProduct(x.Product, x.Price)
                                )
                            )
                        )
                    )
                )
            )
            .ToIResult();
    }

    private static HtmlEl RenderProduct(Product product, ProductPrice price) =>
        Div(
            Attrs("class", "card p-4"),
            Div(
                Div(
                    Attrs("class", "is-size-5"),
                    product.Name
                ),
                Div(
                    Attrs("class", "is-size-7"),
                    product.Description
                )
            ),
            Div(
                Attrs("class", "has-text-right"),
                Div($"{SymbolToHtmlCode(price.Symbol)}{price.Value / 100:N2}")
            )
        );

    private static string SymbolToHtmlCode(string symbol) => symbol switch
    {
        "£" => "&#163;",
        "€" => "&#8364;",
        _ => "",
    };
}