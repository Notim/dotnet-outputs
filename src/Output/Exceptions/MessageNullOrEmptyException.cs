using System;
using System.Runtime.Serialization;

namespace Notim.Outputs.Exceptions;

/// <summary>
/// Classe que encapsula erros para o campo de mensagens no pacote
/// </summary>
[Serializable]
public class MessageNullOrEmptyException : Exception
{

    /// <summary>
    /// 
    /// </summary>
    public MessageNullOrEmptyException()
    { }

    /// <summary>
    /// Construtor que recebe a constant de erro para tipar exception
    /// </summary>
    /// <param name="message"></param>
    public MessageNullOrEmptyException(string message) : base(message)
    { }

    /// <summary>Construtor propagando innerException, se existente.</summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public MessageNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>Construtor para desserialização via remote call.</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected MessageNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

}