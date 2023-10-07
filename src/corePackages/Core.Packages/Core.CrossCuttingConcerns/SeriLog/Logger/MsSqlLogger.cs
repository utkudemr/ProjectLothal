
using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.SeriLog.Logger;

public class MsSqlLogger:BaseLoggerService
{
    private readonly IConfiguration _configuration;
    public MsSqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;
        MsSqlConfiguration logConfiguration = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
           ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions=new MSSqlServerSinkOptions()
        {
            TableName=logConfiguration.TableName,
            AutoCreateSqlDatabase=logConfiguration.AutoCreateSqlTable,
        };

        ColumnOptions columnOptions = new ColumnOptions();

        global::Serilog.Core.Logger seriLog = new LoggerConfiguration()
            .WriteTo
            .MSSqlServer(
                      logConfiguration.ConnectionString
                      ,sinkOptions
                      ,columnOptions:columnOptions)
            .CreateLogger();

        Logger = seriLog;
    }
}
