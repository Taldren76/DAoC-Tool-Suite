using Logger.Enums;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Diagnostics;
using System.Reflection;

namespace Logger
{

    public class LogManager
    {
        public DebugLevel CurrentDebugLevel { get; set; } = DebugLevel.Debug;
        private NLog.Logger NLogLogger { get; set; }
        public string LogFileName { get; private set; } = Properties.Settings.Default.LogFileName;
        
        private string LogFileBasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Taldren, Inc\\DAoC Tool Suite\\";
        public string LogFileLocation => Path.Combine(LogFileBasePath, LogFileName);
        public static LogManager Instance
        {
            get
            {
                lock (thisLock)
                {
                    _Instance ??= new();
                    return _Instance;
                }
            }
        }
        private static LogManager? _Instance = null;
        private static readonly object thisLock = new();
        private static bool? _Initialized = null;
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }

        private static bool Initialized
        {
            get
            {
                lock (thisLock)
                {
                    _Initialized ??= false;
                    return (bool)_Initialized;
                }
            }
            set => _Initialized = value;
        }

        public void Debug<T>(T message)
        {
            lock (thisLock)
            {
                try
                {
                    NLogLogger.Debug<T>(message);
                    if (DebugLevel.Debug >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Debug;
                    }
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    TraceLog($"Attempted to write: {message}");
                }
            }
        }

        public void Warn<T>(T message)
        {
            lock (thisLock)
            {
                try
                {
                    NLogLogger.Warn<T>(message);
                    if (DebugLevel.Warning >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Warning;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        public void Error<T>(T message)
        {
            lock (thisLock)
            {
                try
                {
                    NLogLogger.Error<T>(message);
                    if (DebugLevel.Error >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Error;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
        }

        public string GetPath()
        {
            try
            {
                string path = LogFileLocation;
                TraceLog($"Path: {path}");
                return path;
            }
            catch (Exception ex)
            {
                TraceLog(ex.Message);
                TraceLog(ex.StackTrace);
                return Properties.Settings.Default.LogFileName;
            }
        }

        public LogManager()
        {
            if (!Initialized)
            {
                InitalizeLogger();
            }
            NLogLogger = NLog.LogManager.GetCurrentClassLogger();
            Debug($"Log created at {LogFileLocation}");
        }

        public LogManager(string filename)
        {
            LogFileName = filename;
            if (!Initialized)
            {
                InitalizeLogger();
            }
            NLogLogger = NLog.LogManager.GetCurrentClassLogger();
            Debug($"Log created at {LogFileLocation}");
        }

        private void InitalizeLogger()
        {
            lock (thisLock)
            {
                try
                {
                    // Step 1. Create configuration object 
                    LoggingConfiguration config = new();

                    // Step 2. Create targets and add them to the configuration 
                    FileTarget fileTarget = new()
                    {
                        Name = "file",
                        FileName = LogFileLocation,
                        DeleteOldFileOnStartup = true
                    };
                    config.AddTarget("file", fileTarget);

                    // Step 3. Define rules
                    LoggingRule rule = new("*", LogLevel.Debug, fileTarget);
                    config.LoggingRules.Add(rule);

                    // Step 4. Activate the configuration
                    NLog.LogManager.Configuration = config;
                    Initialized = true;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                }
            }
        }

        public string GetLogContents()
        {
            try
            {
                string contents;
                lock (thisLock)
                {
                    FileAttributes attributes = File.GetAttributes(LogFileLocation);
                    using (FileStream fs = new(LogFileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using StreamReader sr = new(fs);
                        contents = sr.ReadToEnd();
                    }
                    File.SetAttributes(LogFileLocation, attributes);
                }
                return contents;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return string.Empty;
            }
        }
    }
}