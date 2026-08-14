using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Admissions
{
    public sealed record PatientTransferredDomainEvent(AdmissionId AdmissionId, PatientId PatientId, RoomBedId FromBed, RoomBedId ToBed) : DomainEvent;
}
