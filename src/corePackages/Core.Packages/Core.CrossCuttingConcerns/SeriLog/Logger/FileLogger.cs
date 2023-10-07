

using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.SeriLog.Logger;

public class FileLogger:BaseLoggerService
{
    private readonly IConfiguration _configuration;
    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;
        FileLogConfiguration logConfiguration = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        var filePath = string.Format(format:"{0}{1}",arg0:Directory.GetCurrentDirectory()+ logConfiguration.FolderPath,arg1:".txt");

        Logger= new LoggerConfiguration().WriteTo.File(
            filePath,
            rollingInterval:RollingInterval.Day,
            retainedFileCountLimit:null,
            fileSizeLimitBytes: 100000000,
            outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.ffff zzz} [{Level}] {Message}{NewLine}{Exception}"
            ).CreateLogger();
    }
}
