using FluentValidation;

namespace CatalogService.Application.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        RuleFor(c => c.CatalogId).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
    }
}
