using FluentAssertions;
using Notim.Outputs.Exceptions;
using Xunit;

namespace Output.UnitTests.ExceptionsTests;

public class ResultNullExceptionTests    
{
    [Fact]
    public void Constructor_Default_ShouldCreateInstance()
    {
        // Arrange & Act
        var exception = new ResultNullException();

        // Assert
        exception.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithMessage_ShouldCreateInstanceWithMessage()
    {
        // Arrange
        var message = "Test message";

        // Act
        var exception = new ResultNullException(message);

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().Be(message);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldCreateInstanceWithMessageAndInnerException()
    {
        // Arrange
        var message = "Test message";
        var innerException = new Exception("Inner exception message");

        // Act
        var exception = new ResultNullException(message, innerException);

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
    }

}