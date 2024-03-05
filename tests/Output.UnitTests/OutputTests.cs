using Bogus;
using FluentAssertions;
using Notim.Outputs;
using Notim.Outputs.Exceptions;
using Xunit;

namespace Outputs.UnitTests;

public class OutputTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Constructor_Default_ShouldInitializeProperly()
    {
        // Arrange
        var output = new Output<string>();

        // Act

        // Assert
        output.IsValid.Should().BeTrue();
        output.Fault.Should().BeNull();
        output.Messages.Should().BeEmpty();
        output.FaultMessages.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_WithMessageAndResult_ShouldInitializeProperly()
    {
        // Arrange
        var message = _faker.Random.Word();
        var result = _faker.Random.Word();
        var output = new Output<string>(result);

        // Act
        output.AddMessage(message);

        // Assert
        output.IsValid.Should().BeTrue();
        output.Fault.Should().BeNull();
        output.FaultMessages.Should().BeEmpty();
        output.Result.Should().Be(result);
    }
    
    [Fact]
    public void AddErrorMessage_ShouldAddErrorMessageAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var errorMessage = _faker.Random.Word();

        // Act
        output.AddFaultMessage(errorMessage);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Fault.Should().NotBeNull();
        output.FaultMessages.Should().ContainSingle().And.Contain(errorMessage);
        output.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddError_ShouldAddErrorAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var error = new Fault("Custom error message");

        // Act
        output.AddFault(error);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Fault.Should().NotBeNull();
        output.FaultMessages.Should().ContainSingle().And.Contain(error.ErrorMessage);
        output.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddErrorMessages_ShouldAddErrorMessagesAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var errorMessages = _faker.Random.WordsArray(3);

        // Act
        output.AddFaultMessages(errorMessages);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Fault.Should().NotBeNull();
        output.FaultMessages.Should().Contain(errorMessages);
        output.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddMessage_ShouldAddMessage()
    {
        // Arrange
        var output = new Output<string>();
        var message = _faker.Random.Word();

        // Act
        output.AddMessage(message);

        // Assert
        output.IsValid.Should().BeTrue();
        output.Fault.Should().BeNull();
        output.Messages.Should().ContainSingle().And.Contain(message);
        output.FaultMessages.Should().BeEmpty();
    }

    [Fact]
    public void AddMessages_ShouldAddMessages()
    {
        // Arrange
        var output = new Output<string>();
        var messages = _faker.Random.WordsArray(3);

        // Act
        output.AddMessages(messages);

        // Assert
        output.IsValid.Should().BeTrue();
        output.Fault.Should().BeNull();
        output.Messages.Should().Contain(messages);
        output.FaultMessages.Should().BeEmpty();
    }
    
    [Fact]
    public void AddMessages_ShouldAddMessages_MessageNullOrEmptyException()
    {
        // Arrange
        var output = new Output<string>();

        // Act
        var action = () =>
        {
            output.AddMessages(new string[]{null});
        };
        
        // Assert
        action.Should().Throw<MessageNullOrEmptyException>();
    }

    [Fact]
    public void AddErrorMessages_ShouldAddMessages_MessageNullOrEmptyException()
    {
        // Arrange
        var output = new Output<string>();

        // Act
        var action = () =>
        {
            output.AddFaultMessages(new string[]{null});
        };
        
        // Assert
        action.Should().Throw<ErrorMessageNullOrEmptyException>();
    }


    [Fact]
    public void FormatErrorMessages_ShouldReturnConcatenatedErrorMessages()
    {
        // Arrange
        var output = new Output<string>();
        var errorMessages = _faker.Random.WordsArray(3);
        output.AddFaultMessages(errorMessages);

        // Act
        var formattedErrorMessages = output.FormatFaultMessages();

        // Assert
        formattedErrorMessages.Should().Be(string.Join(" | ", errorMessages));
    }

    [Fact]
    public void GetResult_ShouldReturnResult()
    {
        // Arrange
        var output = new Output<string>();
        var result = _faker.Random.Word();
        output.AddResult(result);

        // Act
        var getResult = output.GetResult();

        // Assert
        getResult.Should().Be(result);
    }
    
    [Fact]
    public void GetResult_ShouldReturnResult_ResultNullException()
    {
        // Arrange
        var output = new Output<string>();

        var action = () =>
        {
            output.AddResult(null);
        };

        // Act
        var getResult = output.GetResult();

        // Assert
        action.Should().Throw<ResultNullException>();
    }
    
    [Fact]
    public void StaticBuildSuccess_ShouldInitializeProperly()
    {
        var message = "use case success";
        
        // Arrange
        var output = Output<bool>.WithSuccess(message, true);

        // Act

        // Assert
        output.IsValid.Should().BeTrue();
        output.Fault.Should().BeNull();
        output.Messages.Should().Contain(message);
        output.FaultMessages.Should().BeEmpty();
    }

    [Fact]
    public void StaticBuildError_ShouldInitializeProperly()
    {
        var message = "use case error";
        
        // Arrange
        var output = Output<bool>.WithFault(message);

        // Act

        // Assert
        output.IsValid.Should().BeFalse();
        output.Fault?.ErrorMessage.Should().Be(message);
        output.Fault?.FaultType.Should().Be(FaultType.GenericError);
        output.Fault?.FaultTypeDescription.Should().Be(FaultType.GenericError.ToString());
        output.Messages.Should().BeEmpty();
        output.FaultMessages.Should().Contain(message);
    }

    [Theory]
    [InlineData(FaultType.GenericError)]
    [InlineData(FaultType.InvalidInput)]
    [InlineData(FaultType.Duplicity)]
    [InlineData(FaultType.ExternalServiceUnavailable)]
    [InlineData(FaultType.ResourceNotFound)]
    [InlineData(FaultType.InvalidOperation)]
    public void StaticBuildErrorObject_ShouldInitializeProperly(FaultType faultType)
    {
        var message = "use case error invalid data";
        
        // Arrange
        var output = Output<bool>.WithFault(new Fault(faultType, message));

        // Act

        // Assert
        output.IsValid.Should().BeFalse();
        output.Fault?.ErrorMessage.Should().Be(message);
        output.Fault?.FaultType.Should().Be(faultType);
        output.Fault?.FaultTypeDescription.Should().Be(faultType.ToString());
        output.Messages.Should().BeEmpty();
        output.FaultMessages.Should().Contain(message);
    }

}