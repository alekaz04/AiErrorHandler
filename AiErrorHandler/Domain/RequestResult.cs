namespace AiErrorHandler.Domain;

/// <summary>
/// Результат запроса к ГНС
/// </summary>
internal class RequestResult
{
    /// <summary>
    /// Ответ
    /// </summary>
    public string Response { get; set; } = string.Empty;

    /// <summary>
    /// Количество токенов израсходованных на чтение запроса
    /// </summary>
    public int InputTokens { get; set; }

    /// <summary>
    /// Количество токенов израсходованных на генерацию ответа
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Общее количество израсходованных токенов
    /// </summary>
    public int TotalTokens { get; internal set; }
}
