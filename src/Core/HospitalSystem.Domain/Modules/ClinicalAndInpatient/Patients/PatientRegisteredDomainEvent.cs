using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Patients
{
    public sealed record PatientRegisteredDomainEvent(PatientId PatientId) : DomainEvent;
}
