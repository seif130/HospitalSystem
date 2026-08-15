using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Emergency.TriageRecord.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.EmergencyCase
{
    public sealed class EmergencyCase : AggregateRoot<EmergencyCaseId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DateTime ArrivedOnUtc { get; private set; }
        public bool ArrivedByAmbulance { get; private set; }
        public EmergencyCaseStatus Status { get; private set; }
        public TriageRecord? Triage { get; private set; }

        private EmergencyCase() { }

        private EmergencyCase(EmergencyCaseId id, PatientId patientId, bool arrivedByAmbulance) : base(id)
        {
            PatientId = patientId;
            ArrivedByAmbulance = arrivedByAmbulance;
            ArrivedOnUtc = DateTime.UtcNow;
            Status = EmergencyCaseStatus.Intake;
        }

        public static EmergencyCase Open(PatientId patientId, bool arrivedByAmbulance = false) =>
            new(EmergencyCaseId.New(), patientId, arrivedByAmbulance);

        public void Triage(TriageLevel level, string assessedByStaffId, string presentingComplaint)
        {
            if (Status != EmergencyCaseStatus.Intake) throw new DomainException("Case has already been triaged.");
            Triage = new TriageRecord(level, assessedByStaffId, presentingComplaint);
            Status = EmergencyCaseStatus.Triaged;
            if (level is TriageLevel.Resuscitation or TriageLevel.Emergent)
                RaiseDomainEvent(new CriticalEmergencyCaseTriagedDomainEvent(Id, PatientId, level));
        }

        public void BeginTreatment()
        {
            if (Status != EmergencyCaseStatus.Triaged) throw new DomainException("Case must be triaged before treatment begins.");
            Status = EmergencyCaseStatus.InTreatment;
        }

        public void AdmitToInpatient()
        {
            if (Status != EmergencyCaseStatus.InTreatment) throw new DomainException("Case must be in treatment before admission.");
            Status = EmergencyCaseStatus.AdmittedToInpatient;
        }

        public void Discharge()
        {
            if (Status != EmergencyCaseStatus.InTreatment) throw new DomainException("Case must be in treatment before discharge.");
            Status = EmergencyCaseStatus.Discharged;
        }
    }

}
