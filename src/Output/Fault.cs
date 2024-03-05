using System;
using System.Diagnostics.CodeAnalysis;

namespace Notim.Outputs;

[ExcludeFromCodeCoverage]
public readonly struct Fault : IEquatable<Fault>
{

    public Fault(FaultType faultType, string errorMessage)
    {
        FaultType = faultType;
        ErrorMessage = errorMessage;
    }

    public Fault(string errorMessage)
    {
        FaultType = Notim.Outputs.FaultType.GenericError;
        ErrorMessage = errorMessage;
    }

    public FaultType FaultType { get; }

    public string FaultTypeDescription => FaultType.ToString();

    public string ErrorMessage { get; }

    public bool Equals(Fault other) => FaultType == other.FaultType && ErrorMessage == other.ErrorMessage;

    public override bool Equals(object obj) => obj is Fault other && Equals(other);

    public override int GetHashCode() => FaultType.GetHashCode();

    public static bool operator ==(Fault left, Fault right) => left.Equals(right);

    public static bool operator !=(Fault left, Fault right) => !(left == right);

}