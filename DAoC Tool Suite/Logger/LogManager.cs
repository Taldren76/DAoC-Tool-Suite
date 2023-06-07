using NLog;
using NLog.Targets;
using System.Reflection;
using System.Configuration;

namespace Logger
{
    public class LogManager
    {
        private static readonly object thisLock = new();
        public readonly NLog.Logger NLogLogger = NLog.LogManager.GetCurrentClassLogger();
        private static bool? _Initialized = null;
        public string PATH { get; set; } = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Log.log";
        private static LogManager? _Instance = null;
        public static LogManager Instance
        {
            get
            {
                lock (thisLock)
                {
                    if (_Instance == null)
                        _Instance = new();
                    return _Instance;
                }
            }
        }

        private static bool Initialized
        {
            get
            {
                _Initialized ??= false;

                return (bool)_Initialized;
            }
            set => _Initialized = value;
        }

        public void Debug<T>(T message)
        {
            lock (thisLock)
            {
                NLogLogger.Debug<T>(message);
            }
        }

        public void Warn<T>(T message)
        {
            lock (thisLock)
            {
                NLogLogger.Warn<T>(message);
            }
        }

        public void Error<T>(T message)
        {
            lock (thisLock)
            {
                NLogLogger.Error<T>(message);
            }
        }

        public LogManager()
        {
            //     $"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\\DAoCToolSuite.log";
            //     $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\DAoCToolSuite.log";
            PATH = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{Properties.Settings.Default.LogFileName}";
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

                NLog.Config.LoggingConfiguration configuration = NLog.LogManager.Configuration;
                FileTarget fileTarget = new()
                {
                    Name = "file",
                    FileName = PATH,
                    DeleteOldFileOnStartup = true
                };

                NLog.LogManager.ReconfigExistingLoggers();

                Initialized = true;
            }
        }

        public string GetLogContents()
        {
            string contents;
            lock (thisLock)
            {
                FileAttributes attributes = File.GetAttributes(Instance.PATH);
                using (FileStream fs = new(Instance.PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using StreamReader sr = new(fs);
                    contents = sr.ReadToEnd();
                }
                File.SetAttributes(Instance.PATH, attributes);
            }
            return contents;
        }
    }
}