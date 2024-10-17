using BackendTest_WebAPI.Abstractions.EntityBase;
using BackendTest_WebAPI.Services.Product;

namespace BackendTest_WebAPI.Entities;

public class Product : EntityBase<int>
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public void Update(Command.UpdateProductCommand product)
    {
        Name = product.Name;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
    }

    public static Product Create(Command.CreateProductCommand product)
    {
        return new Product
        {
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }
}
