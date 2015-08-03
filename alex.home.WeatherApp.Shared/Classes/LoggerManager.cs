using System;
using System.Collections.Generic;

using log4net;
using log4net.Appender;

namespace alex.home.WeatherApp.Shared
{
    /// <summary>
    /// Wrapper class for a logging framework, currently using log4net
    /// </summary>
    public static class LoggerManager
    {
        // For more into about log4net, see http://www.eyecatch.no/blog/2012/08/logging-with-log4net-in-c-sharp/

        #region Fields

        private static volatile Dictionary<Type, ILog>  _typeLookup = new Dictionary<Type, ILog>();
        private static readonly object                  Locker      = new object();
        private static string                           _logFile    = null;

        private enum LogMessageType
        {
            Warning,
            Error,
            Info,
            Debug,
            Fatal,
            Other
        }

        #endregion Fields

        #region Public Methods

        public static string GetLogFile()
        {
            return _logFile;
        }

        /// <summary>
        /// Initialises the lgging framework. Optionally, can specify the target log file. The defule log file is: "dsLogs.log"
        /// </summary>
        /// <param name="targetFile"></param>
        public static void Initialise(string targetFile = null)
        {
            // If no file is specified, then setup a default file where this DLL exist
            if (string.IsNullOrWhiteSpace(targetFile)) targetFile = Definitions.DefaultLogFile;
            
            log4net.Config.XmlConfigurator.Configure();

            var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (IAppender a in hierarchy.Root.Appenders)
            {
                if (a is RollingFileAppender)
                {
                    var fa = (RollingFileAppender)a;

                    fa.File = targetFile;
                    fa.ActivateOptions();

                    _logFile = targetFile;
                    break;
                }
            }
        }
        public static void End()
        {
        }

        /// <summary>
        /// Write an error message to the log file
        /// </summary>
        /// <param name="sourceType">The originating class</param>
        /// <param name="argFormat">Error message, which may be in formatted</param>
        /// <param name="args">Optional arguments</param>
        /// <returns></returns>
        public static string WriteError(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Error, sourceType, argFormat, args);
        }
        public static string WriteWarning(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Warning, sourceType, argFormat, args);
        }
        public static string WriteInfo(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Info, sourceType, argFormat, args);
        }
        public static string WriteFatal(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Fatal, sourceType, argFormat, args);
        }
        public static string WriteDebug(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Debug, sourceType, argFormat, args);
        }
        public static string Write(Type sourceType, string argFormat, params object[] args)
        {
            return Write(LogMessageType.Other, sourceType, argFormat, args);
        }

        #endregion Public Methods

        #region Private Methods

        private static string Write(LogMessageType logMessageType, Type sourceType, string argFormat, params object[] args)
        {
            // Ensure we have a target log file
            if (string.IsNullOrEmpty(_logFile)) Initialise();

            if (string.IsNullOrEmpty(argFormat)) return null;

            // Format the message
            var theTrace = string.Format(argFormat, args);

            // Determine the log object to use
            ILog sourceLog;
            if (!_typeLookup.TryGetValue(sourceType, out sourceLog))
            {
                // Use double-checked locking to add the new type
                lock (Locker)
                {
                    if (!_typeLookup.TryGetValue(sourceType, out sourceLog))
                    {
                        sourceLog = LogManager.GetLogger(sourceType);
                        _typeLookup.Add(sourceType, sourceLog);
                    }
                }
            }

            // Log the message
            switch (logMessageType)
            {
                case LogMessageType.Warning: if (sourceLog.IsWarnEnabled)  sourceLog.Warn(theTrace);  break;
                case LogMessageType.Error:   if (sourceLog.IsErrorEnabled) sourceLog.Error(theTrace); break;
                case LogMessageType.Info:    if (sourceLog.IsInfoEnabled)  sourceLog.Info(theTrace);  break;
                case LogMessageType.Debug:   if (sourceLog.IsDebugEnabled) sourceLog.Debug(theTrace); break;
                case LogMessageType.Fatal:   if (sourceLog.IsFatalEnabled) sourceLog.Fatal(theTrace); break;
                default:                     if (sourceLog.IsInfoEnabled)  sourceLog.Info(theTrace);  break;

            }

            return theTrace;
        }

        #endregion Private Methods
    }
}
