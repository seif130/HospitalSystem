using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.SystemLog.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.SystemLog
{

    public sealed class SystemLog : AggregateRoot<SystemLogId>
    {
        public LogSeverity Severity { get; private set; }
        public string Source { get; private set; } = null!;
        public string Message { get; private set; } = null!;
        public DateTime OccurredOnUtc { get; private set; }

        private SystemLog() { }

        private SystemLog(SystemLogId id, LogSeverity severity, string source, string message) : base(id)
        {
            Severity = severity;
            Source = source;
            Message = message;
            OccurredOnUtc = DateTime.UtcNow;
        }

        public static SystemLog Record(LogSeverity severity, string source, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new DomainException("Log message cannot be empty.");
            return new SystemLog(SystemLogId.New(), severity, source.Trim(), message.Trim());
        }
        // Intentionally has no mutating behavior beyond creation — log entries are immutable once written.
    }
}
