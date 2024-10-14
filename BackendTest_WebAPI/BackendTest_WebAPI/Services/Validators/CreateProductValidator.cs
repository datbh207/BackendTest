using FluentValidation;

namespace BackendTest_WebAPI.Services.Validators;

public class CreateProductValidator : AbstractValidator<Command.CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price > 0");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock >= 0");
    }
}
