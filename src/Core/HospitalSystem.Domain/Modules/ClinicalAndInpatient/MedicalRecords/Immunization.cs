using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed class Immunization
    {
        public string VaccineName { get; private set; }
        public DateTime AdministeredOnUtc { get; private set; }
        public string AdministeredByStaffId { get; private set; }
        public DateTime? NextDoseDueUtc { get; private set; }

        internal Immunization(string vaccineName, DateTime administeredOnUtc, string administeredByStaffId, DateTime? nextDoseDueUtc)
        {
            if (string.IsNullOrWhiteSpace(vaccineName))
                throw new DomainException("Vaccine name is required.");

            if (administeredOnUtc > DateTime.UtcNow)
                throw new DomainException("Administered date cannot be in the future.");

            VaccineName = vaccineName.Trim();
            AdministeredOnUtc = administeredOnUtc;
            AdministeredByStaffId = administeredByStaffId;
            NextDoseDueUtc = nextDoseDueUtc;
        }
    }
}
