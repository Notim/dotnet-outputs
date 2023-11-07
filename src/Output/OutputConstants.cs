namespace Notim.Outputs;

public static class OutputConstants
{

    /// <summary>
    /// Variável de retorno indicando que não foi possível incluir erros à lista de erros.
    /// </summary>
    public const string ErrorMessageIsNullOrEmptyMessage = "Error while trying to add string to ErrorMessage Collection. It is null or empty, please verify.";

    /// <summary>
    /// Variável de retorno indicando que não foi possível incluir mensagens à lista.
    /// </summary>
    public const string MessageIsNullOrEmptyMessage = "Error while trying to add string to Message Collection. It is null or empty, please verify.";

    /// <summary>
    /// Variável de retorno indicando que o objeto de Output retornou null.
    /// </summary>
    public const string ResultNullMessage = "Result object is null, please verify.";

    /// <summary>
    /// Variável de retorno indicando que o processo de validação (FailFast) retornou null.
    /// </summary>
    public const string ValidationResultNullMessage = "ValidationResult is null, please verify.";

}