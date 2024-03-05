using System.Linq;
using FluentValidation.Results;
using Notim.Outputs.Exceptions;

namespace Notim.Outputs.FluentValidation;

public static class OutputBaseExtensions
{
    
    public static void AddValidationResult(this Output output, ValidationResult validationResult)
    {
        if (validationResult is null)
            throw new ValidationResultNullException(message: OutputConstants.ValidationResultNullMessage);
        
        var errors = validationResult.Errors.Select<ValidationFailure, string>(e => $"{e.PropertyName} => {e.ErrorMessage}").ToList<string>();
        
        output.AddFault(new Fault(FaultType.InvalidInput, string.Join(",", errors)));
    }
    
    public static void AddValidationResults(this Output output, params ValidationResult[] validationResults)
    {
        foreach (var validationResult in validationResults)
        {
            output.AddValidationResult(validationResult);
        }
    }

}