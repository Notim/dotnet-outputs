namespace Notim.Outputs;

/// <summary>
/// When your use case is an HttpRequest you can filter your HTTP status code depending on the type of error,
/// but you can also use these types of errors when you are using an event based architecture using messaging —
/// you can control when to reprocess the message or ignore the message depending on the type of error returned in the output.
/// </summary>
public enum FaultType
{
    /// <summary>
    /// Error when received data is invalid.
    /// Status code similar: 422, 400
    /// Events retryable: not retryable
    /// </summary>
    InvalidInput,

    /// <summary>
    /// Error when you try to include data but it is already inserted before.
    /// Status code similar: 409
    /// Events retryable: not retryable
    /// </summary>
    Duplicity,

    /// <summary>
    /// Error when an external dependency is unreachable or returned an unexpected failure.
    /// Status code similar: 502, 503
    /// Events retryable: retryable
    /// </summary>
    ExternalServiceUnavailable,

    /// <summary>
    /// When the requested resource cannot be found.
    /// Status code similar: 404, 204
    /// Events retryable: retryable / not retryable (depends on implementation)
    /// </summary>
    ResourceNotFound,

    /// <summary>
    /// When the received data cannot be processed because a business rule is not satisfied.
    /// Status code similar: 400
    /// Events retryable: not retryable
    /// </summary>
    InvalidOperation,

    /// <summary>
    /// When the caller is not authenticated or the token is missing or invalid.
    /// Status code similar: 401
    /// Events retryable: not retryable
    /// </summary>
    Unauthorized,

    /// <summary>
    /// When the caller is authenticated but does not have permission to access the resource or perform the operation.
    /// Status code similar: 403
    /// Events retryable: not retryable
    /// </summary>
    Forbidden,

    /// <summary>
    /// When the current state of the resource conflicts with the requested operation.
    /// Different from Duplicity, which is about unique constraint violations on creation.
    /// Status code similar: 409
    /// Events retryable: retryable / not retryable (depends on implementation)
    /// </summary>
    Conflict,

    /// <summary>
    /// When a required precondition for the operation is not satisfied by the current state of the system.
    /// Status code similar: 412
    /// Events retryable: not retryable
    /// </summary>
    PreconditionFailed,

    /// <summary>
    /// When the caller has exceeded an allowed quota, rate limit, or usage threshold.
    /// Status code similar: 429
    /// Events retryable: retryable (after cooldown period)
    /// </summary>
    LimitExceeded,

    /// <summary>
    /// When the operation requires an active paid subscription or entitlement that the caller does not have.
    /// Status code similar: 402
    /// Events retryable: not retryable
    /// </summary>
    PaymentRequired,

    /// <summary>
    /// When processing encounters an unmapped problem such as an unexpected exception.
    /// Status code similar: 400, 500, 502, 503, 504
    /// Events retryable: retryable / not retryable (depends on implementation)
    /// </summary>
    GenericError
}