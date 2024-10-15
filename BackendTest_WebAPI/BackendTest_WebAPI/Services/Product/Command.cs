namespace BackendTest_WebAPI.Services.Product;

public class Command
{
    public record CreateProduct(
        string Name,
        decimal Price,
        int StockQuantity);

    public record UpdateProduct(
        string Name,
        decimal Price,
        int StockQuantity);

    public record DeleteProduct(
        int Id);
}
