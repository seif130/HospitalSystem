using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Prescriptions
{
    public sealed record PrescriptionIssuedDomainEvent(PrescriptionId PrescriptionId, PatientId PatientId) : DomainEvent;
}
