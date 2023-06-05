using System.IO;
using NLog;
using NLog.Targets;

namespace DAoCToolSuite.Logging
{
    public class Logger
    {
        private readonly object thisLock = new();
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static bool? _Iniialized = null;
        private static bool Initialized
        {
            get
            {
                _Iniialized ??= false;

                return (bool)_Iniialized;
            }
            set => _Iniialized = value;
        }

        public void Debug<T>(T message)
        {
            lock (thisLock)
            {
                logger.Debug<T>(message);
            }
        }

        public void Warn<T>(T message)
        {
            lock (thisLock)
            {
                logger.Warn<T>(message);
            }
        }

        public void Error<T>(T message)
        {
            lock (thisLock)
            {
                logger.Error<T>(message);
            }
        }

        public Logger()
        {
            InitalizeLogger();
        }

        public void InitalizeLogger()
        {
            lock (thisLock)
            {
                if (Initialized)
                {
                    return;
                }
                //Path.GetDirectoryName(Application.ExecutablePath)
                string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\DAoCToolSuite.log"; //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                NLog.Config.LoggingConfiguration configuration = LogManager.Configuration;
                FileTarget fileTarget = new()
                {
                    Name = "file",
                    FileName = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\DAoCToolSuite.log",
                    DeleteOldFileOnStartup = true
                };

                LogManager.ReconfigExistingLoggers();

                Initialized = true;
            }
        }
    }
}