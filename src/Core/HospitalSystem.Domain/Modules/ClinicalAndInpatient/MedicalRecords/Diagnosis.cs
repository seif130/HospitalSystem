using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed record Diagnosis(string Code, string Description, DateTime DiagnosedOnUtc);
}
