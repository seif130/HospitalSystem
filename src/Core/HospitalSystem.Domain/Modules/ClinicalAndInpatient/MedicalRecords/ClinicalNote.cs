using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed record ClinicalNote(string AuthorName, string Text, DateTime WrittenOnUtc);
}
