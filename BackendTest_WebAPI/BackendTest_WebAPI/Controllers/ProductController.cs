using BackendTest_WebAPI.Abstractions.Repositories;
using BackendTest_WebAPI.Entities;
using BackendTest_WebAPI.Services.Product;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BackendTest_WebAPI.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;
    private readonly IValidator<Command.CreateProductCommand> createProductValidator;
    private readonly IValidator<Command.UpdateProductCommand> updateProductValidator;
    public ProductController(
        IProductRepository productRepository,
        IValidator<Command.CreateProductCommand> createProductValidator,
        IValidator<Command.UpdateProductCommand> updateProductValidator
        )
    {
        this.productRepository = productRepository;
        this.createProductValidator = createProductValidator;
        this.updateProductValidator = updateProductValidator;
    }


    [HttpGet]
    public async Task<IActionResult> Products()
    {
        var products = await productRepository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public IActionResult Products([FromRoute] int id)
    {

        var product = productRepository.GetById(id);
        if (product is null)
            return NotFound("Product Not Found");

        return Ok(product);
    }


    [HttpPost]
    public async Task<IActionResult> Products([FromBody] Command.CreateProductCommand request)
    {

        var validationResult = await createProductValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(ConvertErrorToResponse(validationResult));

        var product = Product.Create(request);

        await productRepository.AddAsync(product);
        await productRepository.SaveChangeAsync();

        return Accepted(product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Products([FromRoute] int id, [FromBody] Command.UpdateProductCommand request)
    {
        var validationResult = await updateProductValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(ConvertErrorToResponse(validationResult));


        var product = await productRepository.FindByIdAsync(id);
        if (product is null)
            return NotFound("Product Not Found");

        product.Update(request);
        productRepository.Update(product);

        return Accepted(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {

        var product = await productRepository.FindByIdAsync(id);
        if (product is null)
            return NotFound("Product Not Found");

        productRepository.Remove(product);

        return Accepted();

    }

    private string ConvertErrorToResponse(ValidationResult validationResult)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var error in validationResult.Errors)
        {
            stringBuilder.Append(error.ErrorMessage + ";");
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }
}
