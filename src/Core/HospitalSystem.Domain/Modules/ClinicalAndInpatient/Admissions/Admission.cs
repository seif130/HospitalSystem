using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Clinic.Admissions.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Admissions
{
    public sealed class Admission : AggregateRoot<AdmissionId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId AttendingDoctorId { get; private set; } = null!;
        public RoomBedId RoomBedId { get; private set; } = null!;
        public DateTime AdmittedOnUtc { get; private set; }
        public DateTime? DischargedOnUtc { get; private set; }
        public AdmissionStatus Status { get; private set; }
        public string? DischargeSummaryText { get; private set; }

        private Admission() : base(AdmissionId.New()) { }

        private Admission(AdmissionId id, PatientId patientId, DoctorId attendingDoctorId, RoomBedId roomBedId) : base(id)
        {
            PatientId = patientId;
            AttendingDoctorId = attendingDoctorId;
            RoomBedId = roomBedId;
            AdmittedOnUtc = DateTime.UtcNow;
            Status = AdmissionStatus.Admitted;
        }

        public static Admission Admit(PatientId patientId, DoctorId attendingDoctorId, RoomBedId roomBedId)
        {
            var admission = new Admission(AdmissionId.New(), patientId, attendingDoctorId, roomBedId);
            admission.AddDomainEvent(new PatientAdmittedDomainEvent(admission.Id, patientId, roomBedId));
            return admission;
        }

        public void TransferTo(RoomBedId newRoomBedId)
        {
            if (Status != AdmissionStatus.Admitted) throw new DomainException("Only an active admission can be transferred.");
            var previousBed = RoomBedId;
            RoomBedId = newRoomBedId;
            Status = AdmissionStatus.Transferred;
            AddDomainEvent(new PatientTransferredDomainEvent(Id, PatientId, previousBed, newRoomBedId));
        }

        public void Discharge(string dischargeSummary)
        {
            if (Status == AdmissionStatus.Discharged) throw new DomainException("Admission is already discharged.");
            if (string.IsNullOrWhiteSpace(dischargeSummary)) throw new DomainException("Discharge summary is required.");
            DischargedOnUtc = DateTime.UtcNow;
            DischargeSummaryText = dischargeSummary.Trim();
            Status = AdmissionStatus.Discharged;
        }
    }
}
