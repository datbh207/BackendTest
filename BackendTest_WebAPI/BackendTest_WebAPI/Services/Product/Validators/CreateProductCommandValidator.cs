using FluentValidation;

namespace BackendTest_WebAPI.Services.Product.Validators;

public class CreateProductCommandValidator : AbstractValidator<Command.CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price > 0");

        RuleFor(x => x.StockQuantity)
            .NotNull().WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock >= 0");
    }
}
