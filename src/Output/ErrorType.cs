namespace Notim.Outputs;

/// <summary>
/// when your use case is an HttpRequest you can filter your HTTP status code depending on the type of error but you can also use these types of errors when you are using an event based architecture using messaging you can control when to reprocess the message or ignore the message depending on the type of error returned in the output
/// </summary>
public enum ErrorType
{
    InvalidInput,               // 422, 400           - not retryable
    Duplicity,                  // 409                - not retryable
    ExternalServiceUnavailable, // 502, 503           - retryable
    ResourceNotFound,           // 404, 204           - retryable / not retryable (Depends of implementation)
    GenericError                // 400, 502, 503, 504 - retryable
}