using System.Collections.Generic;
using Notim.Outputs.Exceptions;

namespace Notim.Outputs;

public class Output
{

    private readonly List<string> _messages;
    private readonly List<string> _FaultMessages;

    /// <summary>
    /// public Messages to serialize
    /// </summary>
    public IReadOnlyCollection<string> Messages => _messages.AsReadOnly();

    /// <summary>
    /// public FaultMessages to serialize
    /// </summary>
    public IReadOnlyCollection<string> FaultMessages => _FaultMessages.AsReadOnly();

    /// <summary>
    /// Fault detailed
    /// </summary>
    public Fault? Fault { get; private set; }

    /// <summary>
    /// this field is fulled depending if this have any Fault or message Fault
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    /// default constructor
    /// </summary>
    public Output()
    {
        IsValid = true;
        _messages = new List<string>();
        _FaultMessages = new List<string>();
    }

    /// <summary>
    /// Adding a Fault type with more details
    /// </summary>
    /// <param name="fault">Fault with Fault type with more details</param>
    public void AddFault(Fault fault)
    {
        AddFaultMessages(fault.ErrorMessage.Split(","));
        Fault = fault;

        VerifyValidity();
    }

    /// <summary>
    /// Adding a single Fault message
    /// </summary>
    /// <param name="message">Fault message</param>
    public void AddFaultMessage(string message)
    {
        AddFaultMessages(message);
        Fault = new Fault(message);

        VerifyValidity();
    }

    /// <summary>
    /// Adding list of Fault messages 
    /// </summary>
    /// <param name="messages">Fault message list</param>
    /// <exception cref="FaultMessageNullOrEmptyException"></exception>
    public void AddFaultMessages(params string[] messages)
    {
        Fault = new Fault(string.Join(", ", messages));
        foreach (var message in messages) {
            if (string.IsNullOrEmpty(message))
                throw new ErrorMessageNullOrEmptyException(OutputConstants.ErrorMessageIsNullOrEmptyMessage);

            _FaultMessages.Add(message);
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
        foreach (var message in messages) {
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
    /// Print Fault messages to Logging
    /// </summary>
    /// <returns>formatted Fault with pipe | separation</returns>
    public string FormatFaultMessages() => string.Join(" | ", _FaultMessages);

    private void VerifyValidity() => IsValid = FaultMessages.Count == 0;

}

public class Output<T> : Output
{

    private T _result;

    /// <summary>
    /// public Result to serializer
    /// </summary>
    public T Result => GetResult();

    /// <summary>
    /// default constructor
    /// </summary>
    public Output() : base()
    {
        _result = default(T);
    }

    /// <summary>
    /// Constructor to use Output with definitive Payload 
    /// </summary>
    /// <param name="result">generic T payload</param>
    public Output(object result)
    {
        AddResult((T) result);
    }

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
    /// Build a Output with Fault message
    /// </summary>
    /// <param name="FaultMessage"></param>
    /// <returns>Output build</returns>
    public static Output<T> WithFault(string FaultMessage)
    {
        var outputBuild = new Output<T>();
        outputBuild.AddFaultMessage(FaultMessage);

        return outputBuild;
    }

    /// <summary>
    /// Build a Output with Fault
    /// </summary>
    /// <param name="fault"></param>
    /// <returns>Output build</returns>
    public static Output<T> WithFault(Fault fault)
    {
        var outputBuild = new Output<T>();
        outputBuild.AddFault(fault);

        return outputBuild;
    }

}