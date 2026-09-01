using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.LabAndRadiology.LabResult.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.LabResult
{
    public sealed class LabResult : AggregateRoot<LabResultId>
    {
        public LabOrderId LabOrderId { get; private set; } = null!;
        public string TestCode { get; private set; } = null!;
        public string ResultValue { get; private set; } = null!;
        public string? ReferenceRange { get; private set; }
        public ResultFlag Flag { get; private set; }
        public DateTime ReportedOnUtc { get; private set; }
        public string ReportedByStaffId { get; private set; } = null!;

        private LabResult() { }

        private LabResult(LabResultId id, LabOrderId labOrderId, string testCode, string resultValue,
            string? referenceRange, ResultFlag flag, string reportedByStaffId) : base(id)
        {
            LabOrderId = labOrderId;
            TestCode = testCode;
            ResultValue = resultValue;
            ReferenceRange = referenceRange;
            Flag = flag;
            ReportedByStaffId = reportedByStaffId;
            ReportedOnUtc = DateTime.UtcNow;
        }

        public static LabResult Report(LabOrderId labOrderId, string testCode, string resultValue,
            ResultFlag flag, string reportedByStaffId, string? referenceRange = null)
        {
            if (string.IsNullOrWhiteSpace(resultValue)) throw new DomainException("Result value is required.");
            var result = new LabResult(LabResultId.New(), labOrderId, testCode, resultValue, referenceRange, flag, reportedByStaffId);
            if (flag == ResultFlag.Critical)
                result.AddDomainEvent(new CriticalLabResultReportedDomainEvent(result.Id, labOrderId, testCode));
            return result;
        }
    }
}
