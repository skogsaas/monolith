using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Logging
{
    public class Logger
    {
        #region Instance

        private static Logger instance = null;

        public static Logger Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Logger();
                }

                return instance;
            }
        }

        #endregion

        public static void Trace(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, LogEvent.Severity.Trace, memberName, filePath, lineNumber);
        }

        public static void Info(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, LogEvent.Severity.Info, memberName, filePath, lineNumber);
        }

        public static void Warning(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, LogEvent.Severity.Warning, memberName, filePath, lineNumber);
        }

        public static void Error(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, LogEvent.Severity.Error, memberName, filePath, lineNumber);
        }

        public static void Fatal(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, LogEvent.Severity.Fatal, memberName, filePath, lineNumber);
        }

        private Framework.Channel channel;

        private Logger()
        {
            this.channel = Framework.Manager.Instance.create("Logging");
        }

        private void log(string message, LogEvent.Severity severity, string memberName, string filePath,int lineNumber)
        {
            LogEvent evt = new LogEvent();
            evt.Time = DateTime.Now;
            evt.Member = memberName;
            evt.FilePath = filePath;
            evt.Line = lineNumber;
            evt.Message = message;

            this.channel.publish(evt);
        }
    }
}
