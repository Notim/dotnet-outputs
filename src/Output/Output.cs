using Output.Exceptions;

namespace Output;

public class Output<T>
{

    private readonly List<string> _messages;
    private readonly List<string> _errorMessages;
    private T? _result;

    public IReadOnlyCollection<string> Messages => _messages.AsReadOnly();

    public IReadOnlyCollection<string> ErrorMessages => _errorMessages.AsReadOnly();

    public Error? Error { get; private set; }

    public bool IsValid { get; private set; }

    public T Result => GetResult();

    public Output()
    {
        IsValid = true;
        _messages = new List<string>();
        _errorMessages = new List<string>();
        _result = default(T);
    }

    public Output(object result)
    {
        IsValid = true;
        _messages = new List<string>();
        _errorMessages = new List<string>();
        AddResult((T) result);
    }
    
    public void AddErrorMessage(string message)
    {
        AddErrorMessages(message);
        Error = new Error(message);

        VerifyValidity();
    }

    public void AddError(Error error)
    {
        AddErrorMessages(error.ErrorMessage);
        Error = error;

        VerifyValidity();
    }

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

    public void AddMessage(string message) => AddMessages(message);

    public void AddMessages(params string[] messages)
    {
        foreach (var message in messages)
        {
            if (string.IsNullOrEmpty(message))
                throw new MessageNullOrEmptyException(OutputConstants.MessageIsNullOrEmptyMessage);
            _messages.Add(message);
        }
    }

    public string FormatErrorMessages() => string.Join(" | ", _errorMessages);

    public void AddResult(T result)
    {
        if (result is null)
            throw new ResultNullException(OutputConstants.ResultNullMessage);

        _result = result;
    }
    
    public T GetResult() => _result;

    private void VerifyValidity() => IsValid = ErrorMessages.Count == 0;

}