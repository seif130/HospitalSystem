using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class Doctor : AggregateRoot<DoctorId>
    {
        public PersonName Name { get; private set; } = null!;
        public MedicalSpecialty Specialty { get; private set; }
        public DepartmentId DepartmentId { get; private set; } = null!;
        public string LicenseNumber { get; private set; } = null!;

        private Doctor()
        {
        }

        private Doctor( DoctorId id, PersonName name, MedicalSpecialty specialty,
            DepartmentId departmentId,string licenseNumber) : base(id)
        {
            Name = name;
            Specialty = specialty;
            DepartmentId = departmentId;
            LicenseNumber = licenseNumber;
        }

        public static Doctor Register( PersonName name, MedicalSpecialty specialty,
            DepartmentId departmentId, string licenseNumber )
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new DomainException("License number is required.");

            return new Doctor( DoctorId.New(),name,specialty, departmentId,licenseNumber.Trim());
        }

        public void ChangeName(PersonName name)
        {
            Name = name;
        }

        public void ChangeSpecialty(MedicalSpecialty specialty)
        {
            Specialty = specialty;
        }

        public void ChangeDepartment(DepartmentId departmentId)
        {
            DepartmentId = departmentId;
        }

        public void ChangeLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new DomainException("License number is required.");

            LicenseNumber = licenseNumber.Trim();
        }
    }

}
