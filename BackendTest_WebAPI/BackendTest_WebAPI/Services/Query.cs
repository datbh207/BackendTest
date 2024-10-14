namespace BackendTest_WebAPI.Services;

public class Query
{
    public record GetProduct();

    public record GetProductById(int Id);
}
