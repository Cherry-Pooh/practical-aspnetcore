public static partial class EvenOddLogs
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "input number is even !: {number}")]
    public static partial void LogInformationWhenInputNumberIsEven(this ILogger logger, long number);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "input number is odd !: {number}")]
    public static partial void LogInformationWhenInputNumberIsOdd(this ILogger logger, long number);
}