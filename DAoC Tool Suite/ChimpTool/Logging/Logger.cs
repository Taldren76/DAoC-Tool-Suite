using System.IO;
using System.Reflection;
using NLog;
using NLog.Targets;

namespace DAoCToolSuite.ChimpTool.Logging
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
                string path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\DAoCToolSuite.log";
                NLog.Config.LoggingConfiguration configuration = LogManager.Configuration;
                FileTarget fileTarget = new()
                {
                    Name = "file",
                    FileName = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\DAoCToolSuite.log",
                    DeleteOldFileOnStartup = true
                };

                //MethodCallTarget eventHandler = new()
                //{ 
                //    ClassName = typeof(DAoCToolSuite.ChimpTool.MainForm).AssemblyQualifiedName,
                //    MethodName = "UpdateDataLinkColor"
                //};
                //eventHandler.Parameters.Add(new MethodCallParameter("${level}"));
                //eventHandler.Parameters.Add(new MethodCallParameter("${message}"));

                //configuration.AddRuleForAllLevels(fileTarget);
                //configuration.AddRuleForAllLevels(eventHandler);

                LogManager.ReconfigExistingLoggers();

                Initialized = true;
            }
        }
    }
}