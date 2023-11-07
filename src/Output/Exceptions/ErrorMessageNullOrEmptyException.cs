using System.Runtime.Serialization;

namespace Output.Exceptions;

/// <summary>
/// Classe que encapsula erros para o campo de erros no pacote.
/// </summary>
[Serializable]
public class ErrorMessageNullOrEmptyException : Exception
{

    /// <summary>
    /// 
    /// </summary>
    public ErrorMessageNullOrEmptyException()
    { }

    /// <summary>
    /// Construtor que recebe a constant de erro para tipar exception
    /// </summary>
    /// <param name="message"></param>
    public ErrorMessageNullOrEmptyException(string message) : base(message)
    { }

    /// <summary>Construtor propagando innerException, se existente.</summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ErrorMessageNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>Construtor para desserialização via remote call.</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ErrorMessageNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

}