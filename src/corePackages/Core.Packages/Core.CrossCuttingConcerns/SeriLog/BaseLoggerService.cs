using Serilog;

namespace Core.CrossCuttingConcerns.SeriLog
{
    public abstract class BaseLoggerService
    {
        protected ILogger Logger { get; set; }
        protected BaseLoggerService()
        {
            Logger = null;
        }
        protected BaseLoggerService(ILogger logger)
        {
            Logger = logger;
        }

        public void Error(string message) => Logger.Error(message);
        public void Warning(string message) => Logger.Warning(message);
        public void Information(string message) => Logger.Information(message);
        public void Debug(string message) => Logger.Debug(message);
        public void Critical(string message) => Logger.Fatal(message);
        public void Verbose(string message) => Logger.Verbose(message);
    }
}
