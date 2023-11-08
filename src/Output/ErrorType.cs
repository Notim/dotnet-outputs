namespace Notim.Outputs;

/// <summary>
/// when your use case is an HttpRequest you can filter your HTTP status code depending on the type of error but you can also use these types of errors when you are using an event based architecture using messaging you can control when to reprocess the message or ignore the message depending on the type of error returned in the output
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Error when received data is invalid
    /// Status code similar: 422, 400
    /// Events retryable: not retryable
    /// </summary>
    InvalidInput,
    /// <summary>
    /// Error when you try to include data but is already inserted before
    /// Status code similar: 409
    /// Events retryable: not retryable
    /// </summary>
    Duplicity,
    
    /// <summary>
    /// Status code similar: 502, 503
    /// Events retryable: retryable
    /// </summary>
    ExternalServiceUnavailable,
    
    /// <summary>
    /// When the data search cannot be find
    /// Status code similar: 404, 204
    /// Events retryable: retryable / not retryable (Depends of implementation)
    /// </summary>
    ResourceNotFound,
    
    /// <summary>
    /// When the received data cannot be process cause business rules is not valid
    /// Status code similar: 400
    /// Events retryable: not retryable
    /// </summary>
    OperationInvalid,
    
    /// <summary>
    /// When processing has a not mapped problem (Exceptions thrown)
    /// Status code similar: 400, 502, 503, 504
    /// Events retryable: retryable / not retryable (Depends of implementation)
    /// </summary>
    GenericError
}