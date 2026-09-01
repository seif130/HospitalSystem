using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.RadiologyReport
{
    public sealed class RadiologyReport : AggregateRoot<RadiologyReportId>
    {
        public RadiologyOrderId RadiologyOrderId { get; private set; } = null!;
        public string RadiologistStaffId { get; private set; } = null!;
        public string Findings { get; private set; } = null!;
        public string Impression { get; private set; } = null!;
        public DateTime ReportedOnUtc { get; private set; }

        private readonly List<ImagingAttachment> _attachments = new();
        public IReadOnlyCollection<ImagingAttachment> Attachments => _attachments.AsReadOnly();

        private RadiologyReport() { }

        private RadiologyReport(RadiologyReportId id, RadiologyOrderId orderId, string radiologistStaffId,
            string findings, string impression) : base(id)
        {
            RadiologyOrderId = orderId;
            RadiologistStaffId = radiologistStaffId;
            Findings = findings;
            Impression = impression;
            ReportedOnUtc = DateTime.UtcNow;
        }

        public static RadiologyReport Create(RadiologyOrderId orderId, string radiologistStaffId, string findings, string impression)
        {
            if (string.IsNullOrWhiteSpace(findings)) throw new DomainException("Findings are required.");
            if (string.IsNullOrWhiteSpace(impression)) throw new DomainException("Impression is required.");
            return new RadiologyReport(RadiologyReportId.New(), orderId, radiologistStaffId, findings.Trim(), impression.Trim());
        }

        public void AttachImage(string url, string fileFormat = "DICOM")
        {
            _attachments.Add(new ImagingAttachment(url, fileFormat));
        }
    }
}
