using System;
using System.Runtime.Serialization;

namespace Notim.Outputs.Exceptions;

/// <summary>
/// Classe que encapsula erros de validação para o campo de erros no pacote
/// </summary>
[Serializable]
public class ValidationResultNullException : Exception
{

    /// <summary>
    /// 
    /// </summary>
    public ValidationResultNullException()
    { }

    /// <summary>Construtor propagando message exception</summary>
    /// <param name="message"></param>
    public ValidationResultNullException(string message) : base(message)
    { }

    /// <summary>Construtor propagando innerException, se existente.</summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ValidationResultNullException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>Construtor para desserialização via remote call.</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ValidationResultNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

}