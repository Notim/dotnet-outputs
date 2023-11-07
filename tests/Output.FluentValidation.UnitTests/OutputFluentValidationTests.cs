using FluentAssertions;
using FluentValidation.Results;
using Notim.Outputs;
using Notim.Outputs.Exceptions;
using Notim.Outputs.FluentValidation;
using Xunit;

namespace Outputs.FluentValidation.UnitTests;

public class OutputFluentValidationTests
{

    [Fact]
    public void AddingValidationResult_ShouldCreateInstanceWithMessage()
    {
        // Arrange
        var validationResult = new ValidationResult(new []{
            new ValidationFailure("Field1", "cannot be null"),
            new ValidationFailure("Field2", "cannot be empty"),
            new ValidationFailure("Field3", "is not valid value")
        });

        // Act
        var output = new Output<bool>();

        output.AddValidationResult(validationResult);
        
        // Assert
        output.Result.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.Error?.ErrorType.Should().Be(ErrorType.InvalidInput);
        output.Error?.ErrorMessage.Should().Be(string.Join(",", output.ErrorMessages));
    }
    
    [Fact]
    public void AddingValidationResultFromValidator_ShouldCreateInstanceWithMessage()
    {
        // Arrange
        var simpleObject = new SimpleClass{
            Age = 5,
            Name = string.Empty,
            BirthDate = DateTime.Now
        };

        var validator = new SimpleClassValidator();

        var validationResult = validator.Validate(simpleObject);

        // Act
        var output = new Output<bool>();

        output.AddValidationResult(validationResult);
        
        // Assert
        output.Result.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.Error?.ErrorType.Should().Be(ErrorType.InvalidInput);
        output.Error?.ErrorMessage.Should().Be(string.Join(",", output.ErrorMessages));
    }
    
    [Fact]
    public void AddingValidationResults_ShouldCreateInstanceWithMessage()
    {
        // Arrange

        var listValidation = new ValidationResult[]{
            new ValidationResult(new []{
                new ValidationFailure("Field1", "cannot be null"),
                new ValidationFailure("Field2", "cannot be empty"),
                new ValidationFailure("Field3", "is not valid value")
            }),
            new ValidationResult(new []{
                new ValidationFailure("Field4", "cannot be null"),
                new ValidationFailure("Field5", "cannot be empty"),
                new ValidationFailure("Field6", "is not valid value")
            })
        };
        
        // Act
        var output = new Output<bool>();

        output.AddValidationResults(listValidation);
        
        // Assert
        output.Result.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.Error?.ErrorType.Should().Be(ErrorType.InvalidInput);
    }
    
    
    [Fact]
    public void AddingValidationResultNull_Should_ThrowException()
    {
        // Arrange
        var validationResult = (ValidationResult) null;

        // Act
        var output = new Output<bool>();
        
        var action = () => {
            output.AddValidationResult(validationResult);
        };
        
        // Assert
        action.Should().Throw<ValidationResultNullException>();
    }

}