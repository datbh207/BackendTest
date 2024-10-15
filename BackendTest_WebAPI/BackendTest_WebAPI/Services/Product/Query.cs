namespace BackendTest_WebAPI.Services.Product;

public class Query
{
    public record GetProduct();

    public record GetProductById(int Id);
}
