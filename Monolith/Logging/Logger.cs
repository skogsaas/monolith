using Skogsaas.Legion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Logging
{
    public class Logger
    {
        #region Instance

        internal static Logger instance = null;

        internal static Logger Instance
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
            Instance.log(message, Severity.Trace, memberName, filePath, lineNumber);
        }

        public static void Info(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, Severity.Info, memberName, filePath, lineNumber);
        }

        public static void Warning(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, Severity.Warning, memberName, filePath, lineNumber);
        }

        public static void Error(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, Severity.Error, memberName, filePath, lineNumber);
        }

        public static void Fatal(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Instance.log(message, Severity.Fatal, memberName, filePath, lineNumber);
        }

        internal Channel channel;

        internal Logger()
        {
            this.channel = Manager.Create(Constants.Channel);
        }

        internal void log(string message, Severity severity, string memberName, string filePath,int lineNumber)
        {
            LogEvent evt = this.channel.CreateType<LogEvent>();
            evt.Time = DateTime.Now;
            evt.Member = memberName;
            evt.FilePath = filePath;
            evt.Line = lineNumber;
            evt.Message = message;

            this.channel.Publish(evt);
        }
    }
}
