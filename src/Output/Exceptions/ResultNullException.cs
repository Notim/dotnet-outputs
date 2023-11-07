using System;
using System.Runtime.Serialization;

namespace Notim.Outputs.Exceptions;

[Serializable]
public class ResultNullException : Exception
{

    /// <summary>
    /// 
    /// </summary>
    public ResultNullException()
    { }

    /// <summary>
    /// Construtor que recebe a constant de erro para tipar exception
    /// </summary>
    /// <param name="message"></param>
    public ResultNullException(string message) : base(message)
    { }

    /// <summary>Construtor propagando innerException, se existente.</summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ResultNullException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>Construtor para desserialização via remote call.</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ResultNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

}