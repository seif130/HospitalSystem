using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Admissions
{
    public sealed record PatientAdmittedDomainEvent(AdmissionId AdmissionId, PatientId PatientId, RoomBedId RoomBedId) : DomainEvent;
}
