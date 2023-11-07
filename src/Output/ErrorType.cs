namespace Notim.Outputs;

/// <summary>
/// when your use case is an HttpRequest you can filter your HTTP status code depending on the type of error but you can also use these types of errors when you are using an event based architecture using messaging you can control when to reprocess the message or ignore the message depending on the type of error returned in the output
/// </summary>
public enum ErrorType
{
    InvalidInput,
    Duplicity,
    ExternalServiceUnavailable,
    ResourceNotFound,
    GenericError,
}