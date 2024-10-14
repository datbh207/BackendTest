using BackendTest_WebAPI.Abstractions.Repositories;
using BackendTest_WebAPI.Model;
using BackendTest_WebAPI.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BackendTest_WebAPI.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;
    private readonly IValidator<Command.CreateProduct> createProductValidator;
    private readonly IValidator<Command.UpdateProduct> updateProductValidator;
    public ProductController(
        IProductRepository productRepository,
        IValidator<Command.CreateProduct> createProductValidator,
        IValidator<Command.UpdateProduct> updateProductValidator
        )
    {
        this.productRepository = productRepository;
        this.createProductValidator = createProductValidator;
        this.updateProductValidator = updateProductValidator;
    }


    [HttpGet()]
    public async Task<IActionResult> Products()
    {
        try
        {
            var products = await productRepository.GetAllAsync();

            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult Products([FromRoute] int id)
    {

        try
        {
            var product = productRepository.GetById(id);
            if (product is null)
                return NotFound("Product Not Found");

            return Ok(product);
        }
        catch (ArgumentNullException nullex)
        {
            return NotFound(nullex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Products([FromBody] Command.CreateProduct request)
    {

        try
        {
            var validationResult = await createProductValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(ConvertErrorToResponse(validationResult));

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
            };

            await productRepository.AddAsync(product);

            return Accepted(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Products([FromRoute] int id, [FromBody] Command.UpdateProduct request)
    {
        var validationResult = await updateProductValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(ConvertErrorToResponse(validationResult));


        try
        {
            var product = await productRepository.FindByIdAsync(id);
            if (product is null)
                return NotFound("Product Not Found");

            product.Update(request);
            await productRepository.UpdateAsync(product);

            return Accepted(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        try
        {
            var product = await productRepository.FindByIdAsync(id);
            if (product is null)
                return NotFound("Product Not Found");

            await productRepository.RemoveAsync(product);

            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
