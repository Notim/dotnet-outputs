using FluentValidation.Results;
using Output.Exceptions;

namespace Output.FluentValidation;

public static class OutputBaseExtensions
{
    
    public static void AddValidationResult<T>(this Output<T> output, ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select<ValidationFailure, string>(e => e.ErrorMessage).ToList<string>();
            
        output.AddErrorMessages(errors.ToArray());
        
        output.AddError(new Error(ErrorType.InvalidInput, output.FormatErrorMessages()));
    }
    
    private static void AddValidationResults<T>(this Output<T> output, params ValidationResult[] validationResults)
    {
        foreach (var validationResult in validationResults)
        {
            if (validationResult is null)
                throw new ValidationResultNullException(OutputConstants.ValidationResultNullMessage);

            output.AddValidationResult(validationResult);
        }
    }

}