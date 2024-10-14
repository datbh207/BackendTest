using FluentValidation;

namespace BackendTest_WebAPI.Services.Validators;

public class UpdateProductValidator : AbstractValidator<Command.UpdateProduct>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price > 0");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock >= 0");
    }
}
