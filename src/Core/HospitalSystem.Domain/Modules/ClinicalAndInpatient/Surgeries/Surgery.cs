using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.Surgeries.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Surgeries
{
    public sealed class Surgery : AggregateRoot<SurgeryId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public ClinicRoomId OperatingRoomId { get; private set; } = null!;
        public string Procedure { get; private set; } = null!;
        public DateTime ScheduledForUtc { get; private set; }
        public SurgeryStatus Status { get; private set; }

        private readonly List<SurgicalTeamMember> _team = new();
        public IReadOnlyCollection<SurgicalTeamMember> Team => _team.AsReadOnly();

        private Surgery() : base(SurgeryId.New()) { }

        private Surgery(SurgeryId id, PatientId patientId, ClinicRoomId operatingRoomId, string procedure, DateTime scheduledForUtc) : base(id)
        {
            PatientId = patientId;
            OperatingRoomId = operatingRoomId;
            Procedure = procedure;
            ScheduledForUtc = scheduledForUtc;
            Status = SurgeryStatus.Scheduled;
        }

        public static Surgery Schedule(PatientId patientId, ClinicRoomId operatingRoomId, string procedure, DateTime scheduledForUtc)
        {
            if (scheduledForUtc <= DateTime.UtcNow) throw new DomainException("Surgery must be scheduled in the future.");
            if (string.IsNullOrWhiteSpace(procedure)) throw new DomainException("Procedure name is required.");
            return new Surgery(SurgeryId.New(), patientId, operatingRoomId, procedure, scheduledForUtc);
        }

        public void AssignTeamMember(StaffId staffId, SurgicalRole role)
        {
            if (Status != SurgeryStatus.Scheduled) throw new DomainException("Cannot modify the team once surgery has started.");
            if (role == SurgicalRole.LeadSurgeon && _team.Any(m => m.Role == SurgicalRole.LeadSurgeon))
                throw new DomainException("A lead surgeon is already assigned.");
            _team.Add(new SurgicalTeamMember(staffId, role));
        }

        public void Start()
        {
            if (Status != SurgeryStatus.Scheduled) throw new DomainException("Only a scheduled surgery can start.");
            if (_team.All(m => m.Role != SurgicalRole.LeadSurgeon))
                throw new DomainException("Cannot start surgery without a lead surgeon assigned.");
            Status = SurgeryStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != SurgeryStatus.InProgress) throw new DomainException("Only an in-progress surgery can be completed.");
            Status = SurgeryStatus.Completed;
            AddDomainEvent(new SurgeryCompletedDomainEvent(Id, PatientId));
        }

        public void Cancel()
        {
            if (Status == SurgeryStatus.Completed) throw new DomainException("Cannot cancel a completed surgery.");
            Status = SurgeryStatus.Cancelled;
        }
    }
}
