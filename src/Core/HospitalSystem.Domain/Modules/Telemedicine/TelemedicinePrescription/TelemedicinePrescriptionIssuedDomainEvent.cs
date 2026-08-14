using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Telemedicine.TelemedicinePrescription
{
    public sealed record TelemedicinePrescriptionIssuedDomainEvent(TelemedicinePrescriptionId PrescriptionId, TelemedicineSessionId SessionId, PatientId PatientId) : DomainEvent;

}
