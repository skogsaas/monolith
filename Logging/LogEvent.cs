using Skogsaas.Legion;
using System;

namespace Skogsaas.Monolith.Logging
{
    public enum Severity
    {
        Trace,
        Info,
        Warning,
        Error,
        Fatal
    }

    public interface LogEvent : IEvent
    {
        DateTime Time { get; set; }
        string Member { get; set; }
        string FilePath { get; set; }
        int Line { get; set; }
        Severity Level { get; set; }
        string Message { get; set; }
    }
}
