using FluentValidation;

namespace Outputs.FluentValidation.UnitTests;

public class SimpleClassValidator : AbstractValidator<SimpleClass>
{

    public SimpleClassValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("name cannot be empty");
        
        RuleFor(x => x.Age)
            .GreaterThan(18).WithMessage("age cannot be under 18");
        
        RuleFor(x => x.BirthDate)
            .Must(x => x <= DateTime.Now.AddYears(-18)).WithMessage("date cannot be under 18");
    }

}