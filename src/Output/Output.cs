using System.Collections.Generic;
using Notim.Outputs.Exceptions;

namespace Notim.Outputs;

public class Output<T>
{

    private readonly List<string> _messages;
    private readonly List<string> _errorMessages;
    private T _result;
    
    /// <summary>
    /// public Messages to serialize
    /// </summary>
    public IReadOnlyCollection<string> Messages => _messages.AsReadOnly();

    /// <summary>
    /// public ErrorMessages to serialize
    /// </summary>
    public IReadOnlyCollection<string> ErrorMessages => _errorMessages.AsReadOnly();

    /// <summary>
    /// Error detailed
    /// </summary>
    public Error? Error { get; private set; }

    /// <summary>
    /// this field is fulled depending if this have any error or message error
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    /// public Result to serializer
    /// </summary>
    public T Result => GetResult();

    /// <summary>
    /// default constructor
    /// </summary>
    public Output()
    {
        IsValid = true;
        _messages = new List<string>();
        _errorMessages = new List<string>();
        _result = default(T);
    }

    /// <summary>
    /// Constructor to use Output with definitive Payload 
    /// </summary>
    /// <param name="result">generic T payload</param>
    public Output(object result)
    {
        IsValid = true;
        _messages = new List<string>();
        _errorMessages = new List<string>();
        AddResult((T) result);
    }
    
    /// <summary>
    /// Adding a Error type with more details
    /// </summary>
    /// <param name="error">Error with error type with more details</param>
    public void AddError(Error error)
    {
        AddErrorMessages(error.ErrorMessage.Split(","));
        Error = error;

        VerifyValidity();
    }

    /// <summary>
    /// Adding a single error message
    /// </summary>
    /// <param name="message">error message</param>
    public void AddErrorMessage(string message)
    {
        AddErrorMessages(message);
        Error = new Error(message);

        VerifyValidity();
    }

    /// <summary>
    /// Adding list of error messages 
    /// </summary>
    /// <param name="messages">error message list</param>
    /// <exception cref="ErrorMessageNullOrEmptyException"></exception>
    public void AddErrorMessages(params string[] messages)
    {
        Error = new Error(string.Join(", ", messages));
        foreach (var message in messages)
        {
            if (string.IsNullOrEmpty(message))
                throw new ErrorMessageNullOrEmptyException(OutputConstants.ErrorMessageIsNullOrEmptyMessage);

            _errorMessages.Add(message);
        }

        VerifyValidity();
    }

    /// <summary>
    /// Adding a single message
    /// </summary>
    /// <param name="message">success message</param>
    public void AddMessage(string message) => AddMessages(message);

    /// <summary>
    /// Adding list of success messages 
    /// </summary>
    /// <param name="messages">success messages</param>
    /// <exception cref="MessageNullOrEmptyException"></exception>
    public void AddMessages(params string[] messages)
    {
        foreach (var message in messages)
        {
            if (string.IsNullOrEmpty(message))
                throw new MessageNullOrEmptyException(OutputConstants.MessageIsNullOrEmptyMessage);
            _messages.Add(message);
        }
    }

    /// <summary>
    /// Print success messages to Logging
    /// </summary>
    /// <returns>formatted success messages with pipe</returns>
    public string FormatMessages() => string.Join(" | ", _messages);
    
    /// <summary>
    /// Print error messages to Logging
    /// </summary>
    /// <returns>formatted error with pipe | separation</returns>
    public string FormatErrorMessages() => string.Join(" | ", _errorMessages);

    /// <summary>
    /// Add payload result 
    /// </summary>
    /// <param name="result">generic T payload</param>
    /// <exception cref="ResultNullException">if result is null</exception>
    public void AddResult(T result)
    {
        if (result is null)
            throw new ResultNullException(OutputConstants.ResultNullMessage);

        _result = result;
    }
    
    /// <summary>
    /// Get Result Payload
    /// </summary>
    /// <returns>Resul Payload</returns>
    public T GetResult() => _result;

    /// <summary>
    /// Build a Output with success payload
    /// </summary>
    /// <param name="message"></param>
    /// <param name="payload"></param>
    /// <returns>Output build</returns>
    public static Output<T> WithSuccess(string message, T payload)
    {
        var outputBuild = new Output<T>(payload);
        outputBuild.AddMessage(message);

        return outputBuild;
    }

    /// <summary>
    /// Build a Output with error message
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns>Output build</returns>
    public static Output<T> WithError(string errorMessage)
    {
        var outputBuild = new Output<T>();
        outputBuild.AddErrorMessage(errorMessage);
        
        return outputBuild;
    }
    
    /// <summary>
    /// Build a Output with Error
    /// </summary>
    /// <param name="error"></param>
    /// <returns>Output build</returns>
    public static Output<T> WithError(Error error)
    {
        var outputBuild = new Output<T>();
        outputBuild.AddError(error);
        
        return outputBuild;
    }
    
    private void VerifyValidity() => IsValid = ErrorMessages.Count == 0;

}