using AiErrorHandler.Core;
using AiErrorHandler.Models;
using System.Diagnostics.CodeAnalysis;

namespace AiErrorHandler.ConsoleOutput;

internal static class Program
{
    [SuppressMessage("Usage", "CA2201:Не порождайте исключения зарезервированных типов")]
    private static void Main(string[] args)
    {
        AiErrorStaticAnalyse.InitializeGpt(new ConfigureAiHandler()
        {
            Key = args[0],
            Model = "gpt-4o",
        });

        try
        {
            throw new Exception("Тестовая ошибка где то внутри бизнес кода");
        }
        catch (Exception e)
        {
            AiErrorStaticAnalyse.DescribeException(e).GetAwaiter().GetResult();
        }
    }
}
