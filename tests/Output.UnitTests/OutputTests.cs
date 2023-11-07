using Bogus;
using FluentAssertions;
using Notim.Outputs;
using Notim.Outputs.Exceptions;
using Xunit;

namespace Output.UnitTests;

public class OutputTests
{
    private readonly Faker _faker;

    public OutputTests() => _faker = new Faker();

    [Fact]
    public void Constructor_Default_ShouldInitializeProperly()
    {
        // Arrange
        var output = new Output<string>();

        // Act

        // Assert
        output.IsValid.Should().BeTrue();
        output.Error.Should().BeNull();
        output.Messages.Should().BeEmpty();
        output.ErrorMessages.Should().BeEmpty();
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
        output.Error.Should().BeNull();
        output.ErrorMessages.Should().BeEmpty();
        output.Result.Should().Be(result);
    }
    
    [Fact]
    public void AddErrorMessage_ShouldAddErrorMessageAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var errorMessage = _faker.Random.Word();

        // Act
        output.AddErrorMessage(errorMessage);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.ErrorMessages.Should().ContainSingle().And.Contain(errorMessage);
        output.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddError_ShouldAddErrorAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var error = new Error("Custom error message");

        // Act
        output.AddError(error);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.ErrorMessages.Should().ContainSingle().And.Contain(error.ErrorMessage);
        output.Messages.Should().BeEmpty();
    }

    [Fact]
    public void AddErrorMessages_ShouldAddErrorMessagesAndSetError()
    {
        // Arrange
        var output = new Output<string>();
        var errorMessages = _faker.Random.WordsArray(3);

        // Act
        output.AddErrorMessages(errorMessages);

        // Assert
        output.IsValid.Should().BeFalse();
        output.Error.Should().NotBeNull();
        output.ErrorMessages.Should().Contain(errorMessages);
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
        output.Error.Should().BeNull();
        output.Messages.Should().ContainSingle().And.Contain(message);
        output.ErrorMessages.Should().BeEmpty();
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
        output.Error.Should().BeNull();
        output.Messages.Should().Contain(messages);
        output.ErrorMessages.Should().BeEmpty();
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
            output.AddErrorMessages(new string[]{null});
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
        output.AddErrorMessages(errorMessages);

        // Act
        var formattedErrorMessages = output.FormatErrorMessages();

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

}