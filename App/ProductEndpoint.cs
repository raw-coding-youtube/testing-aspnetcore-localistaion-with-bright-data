namespace App;

public class ProductEndpoint
{
    public static IResult Handle(
        int? id,
        HttpContext httpContext
    )
    {
        id ??= 1;
        
        var product = ProductsDatabase.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return Results.BadRequest();
        }

        var culture = httpContext.GetCulture();
        
        var price = product.Prices.FirstOrDefault(x => x.Culture == culture) 
                    ?? product.Prices.First();

        return Results.Ok(new ProductProjection()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CurrencySymbol = price.Symbol,
            Currency = price.Currency,
            Value = price.Value
        });
    }

    public static readonly List<Product> ProductsDatabase = new List<Product>()
    {
        new Product()
        {
            Id = 1,
            Name = "Shirt",
            Description = "This is a T-shirt",
            Prices = new List<ProductPrice>()
            {
                new ProductPrice() { Id = 1, Culture = "en", Symbol = "£", Currency = "GBP", Value = 2000 },
                new ProductPrice() { Id = 2, Culture = "es", Symbol = "€", Currency = "EUR", Value = 2500 },
            }
        },
        new Product()
        {
            Id = 2,
            Name = "Shorts",
            Description = "This are some cool shorts!",
            Prices = new List<ProductPrice>()
            {
                new ProductPrice() { Id = 3, Culture = "en", Symbol = "£", Currency = "GBP", Value = 3000 },
                new ProductPrice() { Id = 4, Culture = "es", Symbol = "€", Currency = "EUR", Value = 3300 },
            }
        },
    };
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public List<ProductPrice> Prices { get; set; } = new();
}

public class ProductPrice
{
    public int Id { get; set; }
    public string Culture { get; set; } = "";
    public string Symbol { get; set; } = "";
    public string Currency { get; set; } = "";
    public int Value { get; set; }
}

public class ProductProjection
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string CurrencySymbol { get; set; } = "";
    public string Currency { get; set; } = "";
    public int Value { get; set; }
    public string ValueDescription => $"{CurrencySymbol}{Value / 100:N2}";
}