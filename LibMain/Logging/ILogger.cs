using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Logging
{
    // 摘要: 
    //     Manages logging.
    //
    // 备注: 
    //     This is a facade for the different logging subsystems.  It offers a simplified
    //     interface that follows IOC patterns and a simplified priority/level/severity
    //     abstraction.
    public interface ILogger
    {
        // 摘要: 
        //     Determines if messages of priority "debug" will be logged.
        bool IsDebugEnabled { get; }
        //
        // 摘要: 
        //     Determines if messages of priority "error" will be logged.
        bool IsErrorEnabled { get; }
        //
        // 摘要: 
        //     Determines if messages of priority "fatal" will be logged.
        bool IsFatalEnabled { get; }
        //
        // 摘要: 
        //     Determines if messages of priority "info" will be logged.
        bool IsInfoEnabled { get; }
        //
        // 摘要: 
        //     Determines if messages of priority "warn" will be logged.
        bool IsWarnEnabled { get; }

        // 摘要: 
        //     Create a new child logger.  The name of the child logger is [current-loggers-name].[passed-in-name]
        //
        // 参数: 
        //   loggerName:
        //     The Subname of this logger.
        //
        // 返回结果: 
        //     The New ILogger instance.
        //
        // 异常: 
        //   System.ArgumentException:
        //     If the name has an empty element name.
        ILogger CreateChildLogger(string loggerName);
        //
        // 摘要: 
        //     Logs a debug message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsDebugEnabled is true.
        //
        // 参数: 
        //   messageFactory:
        void Debug(Func<string> messageFactory);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   message:
        //     The message to log
        void Debug(string message);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Debug(string message, Exception exception);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void DebugFormat(string format, params object[] args);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void DebugFormat(Exception exception, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void DebugFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a debug message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs an error message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsErrorEnabled is true.
        //
        // 参数: 
        //   messageFactory:
        void Error(Func<string> messageFactory);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   message:
        //     The message to log
        void Error(string message);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Error(string message, Exception exception);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void ErrorFormat(string format, params object[] args);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void ErrorFormat(Exception exception, string format, params object[] args);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs an error message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a fatal message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsFatalEnabled is true.
        //
        // 参数: 
        //   messageFactory:
        void Fatal(Func<string> messageFactory);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   message:
        //     The message to log
        void Fatal(string message);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Fatal(string message, Exception exception);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void FatalFormat(string format, params object[] args);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void FatalFormat(Exception exception, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void FatalFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a fatal message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a info message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsInfoEnabled is true.
        //
        // 参数: 
        //   messageFactory:
        void Info(Func<string> messageFactory);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   message:
        //     The message to log
        void Info(string message);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Info(string message, Exception exception);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void InfoFormat(string format, params object[] args);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void InfoFormat(Exception exception, string format, params object[] args);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void InfoFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs an info message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a warn message with lazily constructed message. The message will be
        //     constructed only if the Castle.Core.Logging.ILogger.IsWarnEnabled is true.
        //
        // 参数: 
        //   messageFactory:
        void Warn(Func<string> messageFactory);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   message:
        //     The message to log
        void Warn(string message);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   message:
        //     The message to log
        void Warn(string message, Exception exception);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void WarnFormat(string format, params object[] args);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void WarnFormat(Exception exception, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void WarnFormat(IFormatProvider formatProvider, string format, params object[] args);
        //
        // 摘要: 
        //     Logs a warn message.
        //
        // 参数: 
        //   exception:
        //     The exception to log
        //
        //   formatProvider:
        //     The format provider to use
        //
        //   format:
        //     Format string for the message to log
        //
        //   args:
        //     Format arguments for the message to log
        void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args);
    }
}
