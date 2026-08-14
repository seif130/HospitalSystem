using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed record LifeThreateningAllergyRecordedDomainEvent(MedicalRecordId MedicalRecordId, PatientId PatientId, string Allergen) : DomainEvent;

}
