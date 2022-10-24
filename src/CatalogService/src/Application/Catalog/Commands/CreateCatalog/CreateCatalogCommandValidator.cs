using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;

public class CreateCatalogCommandValidator : AbstractValidator<CreateCatalogCommand>
{
    public CreateCatalogCommandValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}