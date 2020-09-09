using Fruits.Business.Models;
using FluentValidation;

namespace Fruits.Business.Validations
{
    public class FruitValidation: AbstractValidator<Fruit>
    {
        public FruitValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(f => f.Description)
                .MaximumLength(4000);
        }
    }
}