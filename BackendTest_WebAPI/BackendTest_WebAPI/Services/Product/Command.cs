namespace BackendTest_WebAPI.Services.Product;

public class Command
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        int StockQuantity);

    public record UpdateProductCommand(
        string Name,
        decimal Price,
        int StockQuantity);

    public record DeleteProduct(
        int Id);
}
