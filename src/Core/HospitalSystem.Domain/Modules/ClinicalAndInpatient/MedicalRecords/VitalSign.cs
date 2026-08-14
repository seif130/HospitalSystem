using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed record VitalSign(decimal Temperature, int SystolicBp, int DiastolicBp, int PulseBpm, DateTime RecordedOnUtc);
}
