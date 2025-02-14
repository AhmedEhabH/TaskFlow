using Serilog;

namespace TaskFlow.Api.Extensions
{
    public static class LoggingExtensions
    {
        public static void ConfigureLogging(this ILoggingBuilder loggingBuilder)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        }
    }
}
