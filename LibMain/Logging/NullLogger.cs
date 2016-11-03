using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Logging
{
    // 摘要: 
    //     The Null Logger class. This is useful for implementations where you need
    //     to provide a logger to a utility class, but do not want any output from it.
    //      It also helps when you have a utility that does not have a logger to supply.
    public class NullLogger : ILogger
    {
        public static readonly NullLogger Instance;

        static NullLogger()
        {
            Instance = new NullLogger();
        }
        public NullLogger()
        {

        }
        //
        // 摘要: 
        //     No-op.
        public bool IsDebugEnabled { get; }
        //
        // 摘要: 
        //     No-op.
        public bool IsErrorEnabled { get; }
        //
        // 摘要: 
        //     No-op.
        public bool IsFatalEnabled { get; }
        //
        // 摘要: 
        //     No-op.
        public bool IsInfoEnabled { get; }
        //
        // 摘要: 
        //     No-op.
        public bool IsWarnEnabled { get; }

        // 摘要: 
        //     Returns this NullLogger.
        //
        // 参数: 
        //   loggerName:
        //     Ignored
        //
        // 返回结果: 
        //     This ILogger instance.
        public ILogger CreateChildLogger(string loggerName)
        {
            return this;
        }
        public void Debug(Func<string> messageFactory)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   message:
        //     Ignored
        public void Debug(string message)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   message:
        //     Ignored
        public void Debug(string message, Exception exception)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void DebugFormat(string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void DebugFormat(Exception exception, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        public void Error(Func<string> messageFactory)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   message:
        //     Ignored
        public void Error(string message)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   message:
        //     Ignored
        public void Error(string message, Exception exception)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void ErrorFormat(string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void ErrorFormat(Exception exception, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        public void Fatal(Func<string> messageFactory)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   message:
        //     Ignored
        public void Fatal(string message)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   message:
        //     Ignored
        public void Fatal(string message, Exception exception)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void FatalFormat(string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void FatalFormat(Exception exception, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        public void Info(Func<string> messageFactory)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   message:
        //     Ignored
        public void Info(string message)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   message:
        //     Ignored
        public void Info(string message, Exception exception)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void InfoFormat(string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void InfoFormat(Exception exception, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        public void Warn(Func<string> messageFactory)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   message:
        //     Ignored
        public void Warn(string message)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   message:
        //     Ignored
        public void Warn(string message, Exception exception)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void WarnFormat(string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void WarnFormat(Exception exception, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {

        }
        //
        // 摘要: 
        //     No-op.
        //
        // 参数: 
        //   exception:
        //     Ignored
        //
        //   formatProvider:
        //     Ignored
        //
        //   format:
        //     Ignored
        //
        //   args:
        //     Ignored
        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {

        }
    }
}
