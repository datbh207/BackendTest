using BackendTest_WebAPI.Abstractions.EntityBase;
using BackendTest_WebAPI.Services;

namespace BackendTest_WebAPI.Model;

public class Product : EntityBase<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public void Update(Command.UpdateProduct product)
    {
        Name = product.Name;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
    }
}
