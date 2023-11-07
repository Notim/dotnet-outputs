using System.Diagnostics.CodeAnalysis;

namespace Output;

[ExcludeFromCodeCoverage]
public readonly struct Error : IEquatable<Error>
{

    public Error(ErrorType errorType, string errorMessage)
    {
        ErrorType    = errorType;
        ErrorMessage = errorMessage;
    }

    public Error(string errorMessage)
    {
        ErrorType = ErrorType.GenericError;
        ErrorMessage = errorMessage;
    }

    public ErrorType ErrorType { get; }

    public string ErrorMessage { get; }

    public bool Equals(Error other) => ErrorType == other.ErrorType && ErrorMessage == other.ErrorMessage;

    public override bool Equals(object obj) => obj is Error other && Equals(other);

    public override int GetHashCode() => ErrorType.GetHashCode();

    public static bool operator ==(Error left, Error right) => left.Equals(right);

    public static bool operator !=(Error left, Error right) => !(left == right);

}