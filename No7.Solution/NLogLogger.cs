namespace No7.Solution
{
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using ILogger = DataExportLib.ILogger;

    public class NLogLogger : ILogger
    {
        private const string LogFileName = "log.txt";

        private readonly Logger logger;

        public NLogLogger()
        {
            this.logger = LogManager.GetCurrentClassLogger();

            var config = new LoggingConfiguration();
            var logFile = new FileTarget("logfile") { FileName = LogFileName };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);
            LogManager.Configuration = config;
        }

        public void Warn(string message) => this.logger.Warn(message);

        public void Error(string message) => this.logger.Error(message);

        public void Fatal(string message) => this.logger.Fatal(message);

        public void Debug(string message) => this.logger.Debug(message);

        public void Info(string message) => this.logger.Info(message);
    }
}
