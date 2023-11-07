using System.Linq;
using FluentValidation.Results;
using Notim.Outputs.Exceptions;

namespace Notim.Outputs.FluentValidation;

public static class OutputBaseExtensions
{
    
    public static void AddValidationResult<T>(this Output<T> output, ValidationResult validationResult)
    {
        if (validationResult is null)
            throw new ValidationResultNullException(OutputConstants.ValidationResultNullMessage);
        
        var errors = validationResult.Errors.Select<ValidationFailure, string>(e => $"{e.PropertyName} => {e.ErrorMessage}").ToList<string>();
        
        output.AddError(new Error(ErrorType.InvalidInput, string.Join(",", errors)));
    }
    
    public static void AddValidationResults<T>(this Output<T> output, params ValidationResult[] validationResults)
    {
        foreach (var validationResult in validationResults)
        {
            output.AddValidationResult(validationResult);
        }
    }

}