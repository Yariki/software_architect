using FluentValidation;

namespace CatalogService.Application.Product.Commands.AddProduct;
public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(50);
        RuleFor(e => e.CatalogId).NotEmpty().GreaterThan(0);
    }
}
