using FluentValidation;
using Pustok.Business.Helpers;

namespace Pustok.Business.Validators.ProductValidators;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(256).WithMessage("Maximum size 256 ola biler")
            .MinimumLength(3).Must(x => x.Contains("A")).WithMessage("Adin icinde A herfi olmalidir");

        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);

        //RuleForEach(x => x.TagIds).GreaterThanOrEqualTo(1);

        RuleFor(x => x.Image)
          .Must(x => x?.CheckSize(2) ?? true).WithMessage("Image maximum size must be 2 mb")
          .Must(x => x?.CheckType("image") ?? true).WithMessage("You can only upload with image format");
    }
}
