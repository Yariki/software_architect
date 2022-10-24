using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Commands.UpdateCatalog;

public class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
{
    public UpdateCatalogCommandValidator()
    {
        RuleFor(e => e.Id)
            .GreaterThan(0)
            .NotEmpty();
            
        RuleFor(e => e.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}